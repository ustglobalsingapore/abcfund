using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ABCFund.Models;
using Microsoft.Extensions.Hosting;
using ABCFund.Data;

namespace ABCFund.Controllers
{
    public class ReportController : Controller
    {
        private readonly ABCFundContext _context;

        public ReportController(ABCFundContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Act as a search criteria
            var prestigeStockId = 23;
            var globalStockId = 24;
            var stiStockId = 1; // STI = 1, DJI = 12, NASDAQ = 25 
            var djiStockId = 12;
            var nasdaqStockId = 25;
            var dateEnds = DateTime.Today;
            var dateStart = dateEnds.AddDays(-15);
            var prestigeFundId = 1;
            var globalFundId = 2;

            var holdings = _context.Holding.AsQueryable();
            var prestigeHoldings = holdings.Where(s => s.FundId == prestigeFundId).OrderByDescending(s => s.Weight);
            var globalHoldings = holdings.Where(s => s.FundId == globalFundId).OrderByDescending(s => s.Weight);

            // Get Periodics Data
            var periodics = from p in _context.Periodic select p;

            var prestigePeriodic = periodics.Where(s => s.StockId >= prestigeStockId && s.TradeDate <= dateEnds && s.TradeDate >= dateStart).OrderByDescending(s => s.TradeDate);
            var stiPeriodic = periodics.Where(s => s.StockId >= stiStockId && s.TradeDate >= dateStart && s.TradeDate <= dateEnds).OrderByDescending(s => s.TradeDate);

            var globalPeriodic = periodics.Where(s => s.StockId >= globalStockId && s.TradeDate <= dateEnds && s.TradeDate >= dateStart).OrderByDescending(s => s.TradeDate);
            var djiPeriodic = periodics.Where(s => s.StockId >= djiStockId && s.TradeDate >= dateStart && s.TradeDate <= dateEnds).OrderByDescending(s => s.TradeDate);
            var nasdaqPeriodic = periodics.Where(s => s.StockId >= nasdaqStockId && s.TradeDate >= dateStart && s.TradeDate <= dateEnds).OrderByDescending(s => s.TradeDate);

            var dashboardVM = new ReportViewModel
            {
                PrestigeFund = await _context.Fund.FirstOrDefaultAsync(s => s.Id == prestigeFundId),
                GlobalFund = await _context.Fund.FirstOrDefaultAsync(s => s.Id == globalFundId),
                PrestigeHoldings = await prestigeHoldings.ToListAsync(),
                GlobalHoldings = await globalHoldings.ToListAsync(),
                PrestigePeriodic = await prestigePeriodic.FirstOrDefaultAsync(),
                StiPeriodic = await stiPeriodic.FirstOrDefaultAsync(),
                GlobalPeriodic = await globalPeriodic.FirstOrDefaultAsync(),
                DjiPeriodic = await djiPeriodic.FirstOrDefaultAsync(),
                NasaqPeriodic = await nasdaqPeriodic.FirstOrDefaultAsync(),
            };

            return View(dashboardVM);
        }

        public IActionResult GetChartJSON()
        {
            // Act as a search criteria
            var stockId = 23;
            var benchmarkId = 1;
            var dateEnds = DateTime.Today;
            var dateStart = DateTime.Today.AddMonths(-12);

            // Get database context
            var periodics = from p in _context.Periodic select p;

            // Filter stock data by id and start and end trading date
            var stockData = periodics.Where(s => s.StockId == stockId
                            && s.TradeDate >= dateStart
                            && s.TradeDate <= dateEnds);

            // Filter benchmark data by id and start and end trading date
            var benchmarkData = periodics.Where(s => s.StockId == benchmarkId
                                && s.TradeDate >= dateStart
                                && s.TradeDate <= dateEnds);

            // Select Daily Returns and calculate Cumulative Returns
            var stockDailyReturns = stockData.Select(s => s.OneDay).ToArray();
            var benchmarkDailyReturns = benchmarkData.Select(s => s.OneDay).ToArray();

            // List to store Cumulative Returns 
            List<decimal> stockCReturns = new List<decimal>();
            List<decimal> benchmarkCReturns = new List<decimal>();

            // Use a hypotetical value of 100 as base
            decimal lastHValue = 100.00m;
            bool isFirstValue = true; // sets 1st value start from 0 %

            // Calculate the Cumulative Returns of stock
            foreach (decimal dr in stockDailyReturns)
            {
                lastHValue = lastHValue * (1 + dr / 100);
                if (isFirstValue)
                {
                    isFirstValue = false;
                    stockCReturns.Add(0.00m);
                }
                else
                {
                    stockCReturns.Add(Math.Round(lastHValue - 100, 2)); // set to 2 decimal places
                }
            }

            // Reset value to calculate benchmark
            lastHValue = 100.00m;
            isFirstValue = true;

            // Calculate the Cumulative Returns of benchmark
            foreach (decimal dr in benchmarkDailyReturns)
            {
                lastHValue = lastHValue * (1 + dr / 100);
                if (isFirstValue)
                {
                    isFirstValue = false;
                    benchmarkCReturns.Add(0.00m);
                }
                else
                {
                    benchmarkCReturns.Add(Math.Round(lastHValue - 100, 2)); // set to 2 decimal places
                }
            }

            // Use only 1 trade dates for labelling in chart
            var tradeDates = stockData.Select(s => s.TradeDate).ToArray();

            List<String> dateLabels = new List<string>();
            foreach (DateTime dateItem in tradeDates)
            {
                dateLabels.Add(dateItem.ToString("d"));
            }

            // Save datelabels, stock and benchmark CR into a new list of object and returns as JSON 
            List<object> performanceList = new List<object>();
            performanceList.Add(dateLabels);
            performanceList.Add(stockCReturns);
            performanceList.Add(benchmarkCReturns);

            return Json(performanceList);
        }

        public IActionResult GetAllFundSectorJSON()
        {
            int prestigeFundId = 1; // Prestige
            int globalFundId = 2; // Prestige

            // zone = sector & industrym sales_amount = weight
            var prestigeQuery = _context.Holding.Where(s => s.FundId == prestigeFundId)
                            .GroupBy(s => s.Sector)
                            .Select(s => new {
                                sector = s.Key,
                                weight = s.Sum(s => s.Weight)
                            }).OrderByDescending(s => s.weight);

            var globalQuery = _context.Holding.Where(s => s.FundId == globalFundId)
                            .GroupBy(s => s.Sector)
                            .Select(s => new {
                                sector = s.Key,
                                weight = s.Sum(s => s.Weight)
                            }).OrderByDescending(s => s.weight);

            var prestigeSectors = prestigeQuery.Select(s => s.sector).ToArray();
            var prestigeWeights = prestigeQuery.Select(s => s.weight).ToArray();

            var globalSectors = globalQuery.Select(s => s.sector).ToArray();
            var globalWeights = globalQuery.Select(s => s.weight).ToArray();

            List<object> sectorData = new List<object>();
            sectorData.Add(prestigeSectors);
            sectorData.Add(prestigeWeights);
            sectorData.Add(globalSectors);
            sectorData.Add(globalWeights);

            return Json(sectorData);
        }

        public IActionResult GetAllFundIndustryJSON()
        {
            int prestigeFundId = 1; // Prestige
            int globalFundId = 2; // Prestige

            // zone = sector & industrym sales_amount = weight
            var prestigeQuery = _context.Holding.Where(s => s.FundId == prestigeFundId)
                            .GroupBy(s => s.Industry)
                            .Select(s => new {
                                industry = s.Key,
                                weight = s.Sum(s => s.Weight)
                            }).OrderByDescending(s => s.weight);

            var globalQuery = _context.Holding.Where(s => s.FundId == globalFundId)
                            .GroupBy(s => s.Industry)
                            .Select(s => new {
                                industry = s.Key,
                                weight = s.Sum(s => s.Weight)
                            }).OrderByDescending(s => s.weight);

            var prestigeIndustries = prestigeQuery.Select(s => s.industry).ToArray();
            var prestigeWeights = prestigeQuery.Select(s => s.weight).ToArray();

            var globalIndustries = globalQuery.Select(s => s.industry).ToArray();
            var globalWeights = globalQuery.Select(s => s.weight).ToArray();

            List<object> sectorData = new List<object>();
            sectorData.Add(prestigeIndustries);
            sectorData.Add(prestigeWeights);
            sectorData.Add(globalIndustries);
            sectorData.Add(globalWeights);

            return Json(sectorData);
        }

        public IActionResult GetChartJSON2()
        {
            // Act as a search criteria
            int prestigeStockId = 23;
            int globalStockId = 24;
            int stiStockId = 1; // STI = 1, DJI = 12, NASDAQ = 25 
            int djiStockId = 12;
            int nasdaqStockId = 25;
            var dateEnd = DateTime.Today;
            var dateStart = DateTime.Today.AddMonths(-12);

            // Get database context
            var periodics = from p in _context.Periodic select p;

            List<decimal> prestigeCReturns = GetCumulativeReturnsList(prestigeStockId, dateStart, dateEnd);
            List<decimal> stiCReturns = GetCumulativeReturnsList(stiStockId, dateStart, dateEnd);
            List<decimal> globalCReturns = GetCumulativeReturnsList(globalStockId, dateStart, dateEnd);

            List<decimal> djiCReturns = GetCumulativeReturnsList(djiStockId, dateStart, dateEnd);
            List<decimal> nasdaqCReturns = GetCumulativeReturnsList(nasdaqStockId, dateStart, dateEnd);

            List<String> prestigeTradeDateLabels = GetTradeDatesLabels(prestigeStockId, dateStart, dateEnd);

            List<String> globalTradeDateLabels = GetTradeDatesLabels(prestigeStockId, dateStart, dateEnd);

            // Save datelabels, stock and benchmark CR into a new list of object and returns as JSON 
            List<object> performanceList = new List<object>();
            performanceList.Add(prestigeTradeDateLabels);
            performanceList.Add(prestigeCReturns);
            performanceList.Add(stiCReturns);
            performanceList.Add(globalTradeDateLabels);
            performanceList.Add(globalCReturns);
            performanceList.Add(djiCReturns);
            performanceList.Add(nasdaqCReturns);

            return Json(performanceList);
        }

        public List<decimal> GetCumulativeReturnsList(int stockId, DateTime dateStart, DateTime dateEnd)
        {
            // Use a hypotetical value of 100 as base
            decimal lastHValue = 100.00m;
            bool isFirstValue = true; // sets 1st value start from 0 %

            // Get database context
            var periodics = from p in _context.Periodic select p;

            // Filter the 5 periodic data by id and tradeDates
            var stockPeriodics = periodics.Where(s => s.StockId == stockId
                                   && s.TradeDate >= dateStart
                                   && s.TradeDate <= dateEnd);

            // Select Daily Returns and calculate Cumulative Returns
            var dailyReturns = stockPeriodics.Select(s => s.OneDay).ToArray();

            // List to store Cumulative Returns 
            List<decimal> cumulativeReturns = new List<decimal>();


            // Calculate the Cumulative Returns of stock
            foreach (decimal dr in dailyReturns)
            {
                lastHValue = lastHValue * (1 + dr / 100);
                if (isFirstValue)
                {
                    isFirstValue = false;
                    cumulativeReturns.Add(0.00m);
                }
                else
                {
                    cumulativeReturns.Add(Math.Round(lastHValue - 100, 2)); // 2 decimals
                }
            }

            return cumulativeReturns;
        }

        public List<String> GetTradeDatesLabels(int stockId, DateTime dateStart, DateTime dateEnd)
        {
            // Get database context
            var periodics = from p in _context.Periodic select p;

            // Get TradeDates as Label
            var stockPeriodic = periodics.Where(s => s.StockId == stockId
                                   && s.TradeDate >= dateStart
                                   && s.TradeDate <= dateEnd);

            var tradeDates = stockPeriodic.Select(s => s.TradeDate).ToArray();

            List<String> dateLabels = new List<string>();
            foreach (DateTime dateItem in tradeDates)
            {
                dateLabels.Add(dateItem.ToString("d"));
            }

            return dateLabels;
        }
    }
}
