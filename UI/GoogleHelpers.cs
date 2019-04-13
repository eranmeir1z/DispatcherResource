using BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using XX;

namespace UI
{


    public static class GoogleHelpers
    {

        public static string CreateJson(List<ClosestAgent> CA)
        {
            var features = new List<Feature>();

            foreach (var item in CA)
            {
                var f = new Feature
                {
                    type = "Feature",
                    geometry = new Geometry
                    {
                        type = "Point",
                        coordinates = new List<double> { item.ToLng, item.ToLat }
                    },
                    properties = new BL.Models.Properties
                    {
                        name = item.Name,
                        icon = "http://maps.google.com/mapfiles/ms/micons/mechanic.png"
                    }
                };

                features.Add(f);
            }

            var ro = new RootObject
            {
                type = "FeatureCollection",
                features = features
            };

            var result = JsonConvert.SerializeObject(ro);
            return result;
        }


    }
}
