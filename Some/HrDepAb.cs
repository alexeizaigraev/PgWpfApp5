using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgWpfApp5
{
    class HrDepAb : Papa
    {
        static SortedDictionary<string, int> dictSum = new SortedDictionary<string, int>();

        public static void MainHrDepAb()
        {
            #region
            var natasha = MkNatasha();
            //string myKey = "partner";
            //string outText = "№ п/п;\"№ Відділення ТОВ \"\"ЕПС\"\"\";Адреса;koatu1;koatu2;Партнер\n";
            string outText = "№ п/п;\"№ Відділення ТОВ \"\"ЕПС\"\"\";Адреса;Партнер\n";
            var data = GetSummuryAbData();
            var sizeLine = data[0].Count;


            int count = 0;
            foreach (var u in data)
            {
                try
                {
                    if (u[0] == "" || natasha.IndexOf(u[0]) < 0) continue;

                    count++;
                    string outLine = String.Format("{0}", count) + ";"
                            + u[0] + ";" + u[1] + ";" + u[2];

                    outText += outLine + "\n";
                }
                //catch (Exception ex) { pMagenta(ex.Message); }
                catch (Exception ex) { SayRed(ex.Message); }
                //catch { }
            }

            string oFname = dataOutPath + "summury_ab.csv";
            SayGreen($"\n\n\tsumm {count}\n");
            TextToFile(oFname, outText);
            //OpenNote(oFname);
            #endregion
        }

        internal static List<List<string>> GetSummuryAbData()
        {
            #region
            List<List<string>> outList = new List<List<string>>();
            using (var context = new postgresContext())
            {
                var department = context.Departments;
                var terminal = context.Terminals;
                var otbor = context.Otbors;
                var w = department.ToList();

                var lingVar = from dep in department
                              select
                              new
                              {
                                  dep = dep.Department1,
                                  //region = dep.RegionDep,
                                  //disrictRegion = dep.DistrictRegionDep,
                                  //potIndex = dep.PostIndexDep,
                                  //cityType = dep.CityTypeDep,
                                  //city = dep.CityDep,
                                  //districtCity = dep.DistrictCityDep,
                                  //streetType = dep.StreetTypeDep,
                                  //street = dep.StreetDep,
                                  //hous = dep.HousDep,
                                  adres = dep.Address,
                                  //koatu = dep.KoatuDep,
                                  partner = dep.Partner
                              };

                foreach (var line in lingVar)
                {

                    List<string> lineList = new List<string>();
                    lineList.Add(line.dep);
                    lineList.Add(line.adres);
                    lineList.Add(line.partner);

                    outList.Add(lineList);
                }

            }
            return outList;
            #endregion
        }

    }
}
