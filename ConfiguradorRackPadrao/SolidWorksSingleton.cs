using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks;

namespace ConfiguradorRackPadrao
{
    internal class SolidWorksSingleton
    {
        private static SldWorks.SldWorks swApp;

        // Tornar a classe private para não poder ser instanciada.
        private SolidWorksSingleton()
        {
        }

        internal static SldWorks.SldWorks Get_swApp()
        {
            if (swApp == null)
            {
                swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks.SldWorks;
                swApp.Visible = true;
                return swApp;
            }
            return swApp;
        }

        internal async static Task<SldWorks.SldWorks> Get_swAppAsync()
        {
            if (swApp == null)
            {
                return await Task.Run(() => {

                    swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks.SldWorks;
                    swApp.Visible = true;
                    return swApp;
                });
            }
            return swApp;
        }


        internal static void Dispose()
        {
            if (swApp != null)
            {
                swApp.ExitApp();
                swApp = null;
            }
        }


    }
}
