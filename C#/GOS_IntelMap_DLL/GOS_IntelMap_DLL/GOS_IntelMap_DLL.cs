using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.IO;

namespace GOS_IntelMap
{
    public class DllEntry
    {
        // This 2 line are IMPORTANT and if changed will stop everything working
        // To send a string back to ARMA append to the output StringBuilder, ARMA outputSize limit applies!
        [DllExport("_RVExtension@12", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function)
        {
            outputSize--;
            // determine l'operation 
            string result = "";
            /* string[] dataRecu = function.Split(',');

             // Switch Action a mener
             switch (dataRecu[0])
             {
                 case "0":
                     result = dataRecu[1] + "." + DateTime.Now.ToString("yyMMddHHmm") + ".dta";
                     break;
                 case "1": // Reception Time FRAME
                     AjouteFrame(dataRecu);
                     result = "ok";
                     break;
                 case "2": // Reception des données
                     // Action,fichier,nomPlayer,x,y,Dir,alive
                     ReceptionData(dataRecu); 
                     result = "ok";
                     break;
                 default:
                     result = "Error !!!";
                     break;
             }
             */
            new IntelMapWService("http://clan-gos.fr/forum/GOS_IntelMap/GOS_IntelMap_Post.php", function);
            output.Append(result);
        }
        public static void AjouteFrame(string[] param)
        {
            string path = @"D:\DATA\GOSIntel\" + param[1];
            if (!File.Exists(path))
            {
                // Creation du fichier en ecriture
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("[FRAME=" + param[2] + "]");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("[FRAME=" + param[2] + "]");
                }
            }
        }
        public static void ReceptionData(string[] param)
        {
            string path = @"D:\DATA\GOSIntel\" + param[1];
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(param[2] + "," + param[3] + "," + param[4] + "," + param[5] + "," + param[6]);
            }

        }
    }

}