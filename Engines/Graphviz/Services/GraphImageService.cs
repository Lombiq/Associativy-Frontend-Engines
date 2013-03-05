using System;
using System.Net;
using System.Text;
using Associativy.EventHandlers;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Exceptions;
using Orchard.FileSystems.Media;
using Piedone.HelpfulLibraries.Tasks;
using QuickGraph;
using QuickGraph.Graphviz;

namespace Associativy.Frontends.Engines.Graphviz.Services
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class GraphImageService : IGraphImageService
    {
        protected readonly IStorageProvider _storageProvider;
        protected readonly ILockingCacheManager _cacheManager;
        protected readonly IGraphEventMonitor _graphEventMonitor;

        public GraphImageService(
            IStorageProvider storageProvider,
            ILockingCacheManager cacheManager,
            IGraphEventMonitor graphEventMonitor)
        {
            _storageProvider = storageProvider;
            _cacheManager = cacheManager;
            _graphEventMonitor = graphEventMonitor;
        }

        public virtual string ToSvg(IGraphDescriptor graphDescriptor, IUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization)
        {
            //var stringBuilder = new StringBuilder();
            //using (var xmlWriter = XmlWriter.Create(stringBuilder))
            //{
            //    // This may also need attributes on models, see: http://quickgraph.codeplex.com/wikipage?title=GraphML%20Serialization&referringTitle=Documentation
            //    graph.SerializeToGraphML<TNode, IUndirectedEdge<TNode>, IUndirectedGraph<TNode, IUndirectedEdge<TNode>>>(
            //        xmlWriter,
            //        node => node.Label,
            //        edge => edge.Source.ContentItem.Id.ToString() + edge.Target.ContentItem.Id.ToString());
            //}
            //var graphML = stringBuilder.ToString();

            // Sadly graph.GetHashCode() gives different results on different requests, therefore only the dot hash
            // is reliable. Fortunately it's quite fast.
            var dotData = graph.ToGraphviz(algorithm =>
            {
                initialization(algorithm);
            });

            var filePath = "Associativy/Graphs-" + graphDescriptor.Name + "/" + dotData.GetHashCode() + ".svg";

            return _cacheManager.Get("Associativy.Frontends.Graphviz.GraphImages." + filePath, 
                ctx =>
                {
                    _graphEventMonitor.MonitorChanged(graphDescriptor, ctx);

                    return RetrieveImage(dotData, filePath);
                },
                () =>
                {
                    return RetrieveImage(dotData, filePath);
                });
        }

        private string RetrieveImage(string dotData, string filePath)
        {
            // Since there is no method for checking the existence of a file, we use this ugly technique
            try
            {
                _storageProvider.DeleteFile(filePath);
            }
            catch (Exception ex)
            {
                if (ex.IsFatal()) throw;
            }

            using (var wc = new WebClient())
            {
                var svgData = wc.UploadString("http://rise4fun.com/services.svc/ask/agl", dotData);

                using (var stream = _storageProvider.CreateFile(filePath).OpenWrite())
                {
                    var bytes = Encoding.UTF8.GetBytes(svgData);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            return _storageProvider.GetPublicUrl(filePath);
        }
    }
}