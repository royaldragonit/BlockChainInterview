using BlockChainInterview.Data;
using BlockChainInterview.Services;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlockChainInterview.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEtherService _etherService;
        public HomeController(IEtherService etherService)
        {
            _etherService = etherService;
        }
        public async Task<ActionResult> Index()
        {
            int limit = 0;
            for (int i = 12100001; i <= 12100500; i++)
            {
                #region FREE API PLAN 5 request/second

                if (limit % 5 == 0 && limit > 0)
                {
                    //FREE API PLAN 5 request/second
                    await Task.Delay(1000);
                }
                limit++;
                #endregion
                var block = _etherService.GetBlockByNumber(i);
                if (block != null && block.result != null && block.result.uncles.Count > 0)
                {
                    //Convert int to hex
                    var hexNumber = $"0x{i:X}";
                    var blockTransaction = _etherService.GetBlockTransactionCountByNumber(hexNumber);
                    //Convert hex to int
                    if (int.Parse(blockTransaction.result.Replace("0x", ""), NumberStyles.HexNumber) != 0)
                    {
                        var transaction = _etherService.GetTransactionByBlockNumberAndIndex(hexNumber, blockTransaction.result);
                        //Insert to database
                        var db = DBConnection.Instance();
                        if (db.IsConnect())
                        {
                            //suppose col0 and col1 are defined as VARCHAR in the DB
                            string query = "SELECT * FROM blocks";
                            var cmd = new MySqlCommand(query, db.Connection);
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                string someStringFromColumnZero = reader.GetString(0);
                                string someStringFromColumnOne = reader.GetString(1);
                                Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
                            }
                            db.Close();
                        }
                    }
                }

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}