using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL.Models;
using DAL;

namespace BL
{
    public class BLClass
    {


        public List<ClosestAgent> GetCloestAgent()
        {
            SRVReference.SRVClient client = new SRVReference.SRVClient("BasicHttpBinding_ISRV");
            // source location
            // HaNapah Street, Karmiel
            // Latitude = 32.92146 Longitude = 35.31982
            var sourceLat = 32.92146;
            var sourceLng = 35.31982;


            using (DispatcherEntities en = new DispatcherEntities())
            {
                try
                {
                    var q = en.tech_agent
                                .ToList()
                                .Select(s => new ClosestAgent
                                {
                                    Name = s.name,
                                    Distance = client.GetDis(sourceLng, sourceLat, s.longitude, s.latitude),
                                    ToLat = s.latitude,
                                    ToLng = s.longitude
                                })
                                .OrderBy(v => v.Distance)
                                .Take(5);
                    return q.ToList();
                }
                catch (Exception ex)
                {
                    //error occured, we can log it.
                    // an empty list will be returned, for not showing real error to client.
                    MessageBox.Show("Error occured... please try again or fix the error in the log.");
                    return new List<ClosestAgent>();

                }
            }
        }
    }
}
