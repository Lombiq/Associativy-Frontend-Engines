using System;
using System.Net;
using System.Text;
using Associativy.EventHandlers;
using Associativy.Models;
using Associativy.Services;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.FileSystems.Media;
using QuickGraph;
using QuickGraph.Graphviz;

namespace Associativy.FrontendEngines.Engines.Graphviz.Services
{
    [OrchardFeature("Associativy")]
    public class GraphImageService<TGraphDescriptor> : AssociativyServiceBase, IGraphImageService<TGraphDescriptor>
        where TGraphDescriptor : IGraphDescriptor
    {
        protected readonly IStorageProvider _storageProvider;
        protected readonly ICacheManager _cacheManager;
        protected readonly IGraphEventMonitor _graphEventMonitor;

        protected string _storagePath;

        private object _graphDescriptorLocker = new object();
        public override IGraphDescriptor GraphDescriptor
        {
            set
            {
                lock (_graphDescriptorLocker) // This is to ensure that used services also have the same graphDescriptor
                {
                    _storagePath = "Associativy/Graphs-" + value.TechnicalGraphName + "/";
                    base.GraphDescriptor = value;
                }
            }
        }

        public GraphImageService(
            TGraphDescriptor graphDescriptor,
            IStorageProvider storageProvider,
            ICacheManager cacheManager,
            IGraphEventMonitor graphEventMonitor)
            : base(graphDescriptor)
        {
            _storageProvider = storageProvider;
            _cacheManager = cacheManager;
            _graphEventMonitor = graphEventMonitor;
            GraphDescriptor = graphDescriptor;
        }

        public virtual string ToSvg(IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization)
        {
            //var stringBuilder = new StringBuilder();
            //using (var xmlWriter = XmlWriter.Create(stringBuilder))
            //{
            //    // This may also need attributes on models, see: http://quickgraph.codeplex.com/wikipage?title=GraphML%20Serialization&referringTitle=Documentation
            //    graph.SerializeToGraphML<TNode, IUndirectedEdge<TNode>, IMutableUndirectedGraph<TNode, IUndirectedEdge<TNode>>>(
            //        xmlWriter,
            //        node => node.Label,
            //        edge => edge.Source.Id.ToString() + edge.Target.Id.ToString());
            //}
            //var graphML = stringBuilder.ToString();

            // Sadly graph.GetHashCode() gives different results on different requests, therefore only the dot hash
            // is reliable. Fortunately it's quite fast.
            var dotData = graph.ToGraphviz(algorithm =>
            {
                initialization(algorithm);
            });

            var filePath = _storagePath + dotData.GetHashCode() + ".svg";

            return _cacheManager.Get("Associativy.GraphImages." + filePath, ctx =>
            {
                _graphEventMonitor.MonitorChanged(ctx, GraphDescriptor);

                // Since there is no method for checking the existance of a file, we use this ugly technique
                try
                {
                    _storageProvider.DeleteFile(filePath);
                }
                catch (Exception)
                {
                }

                var wc = new WebClient();
                var svgData = wc.UploadString("http://rise4fun.com/services.svc/ask/agl", dotData);

                using (var stream = _storageProvider.CreateFile(filePath).OpenWrite())
                {
                    var bytes = Encoding.UTF8.GetBytes(svgData);
                    stream.Write(bytes, 0, bytes.Length);
                }

                return _storageProvider.GetPublicUrl(filePath);
            });
        }
    }

    [OrchardFeature("Associativy.FrontendEngines")]
    public class GraphImageService : GraphImageService<IGraphDescriptor>, IGraphImageService
    {
        public GraphImageService(
            IGraphDescriptor graphDescriptor,
            IStorageProvider storageProvider,
            ICacheManager cacheManager,
            IGraphEventMonitor graphEventMonitor)
            : base(graphDescriptor, storageProvider, cacheManager, graphEventMonitor)
        {
        }
    }
}