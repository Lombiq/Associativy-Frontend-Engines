using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associativy.Frontends.Models
{
    public class GraphSettings
    {
        public int InitialZoomLevel { get; set; }
        public int ZoomLevelCount { get; set; }
        public int MaxConnectionCount { get; set; }

        private static GraphSettings _default = new GraphSettings();
        public static GraphSettings Default { get { return _default; } }


        public GraphSettings()
        {
            InitialZoomLevel = 0;
            ZoomLevelCount = 10;
            MaxConnectionCount = 200;
        }
    }
}