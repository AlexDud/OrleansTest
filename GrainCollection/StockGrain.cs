namespace GrainCollection
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using GrainInterfaces;
    using Orleans;

    public class StockGrain : Grain, IStockGrain
    {
        string price;
        string graphData;

        public override async Task OnActivateAsync()
        {
            string stock;
            this.GetPrimaryKey(out stock);
            await UpdatePrice(stock);

            var timer = RegisterTimer(
                UpdatePrice,
                stock,
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(1));

            await base.OnActivateAsync();
        }

        async Task UpdatePrice(object stock)
        {
            // collect the task variables without awaiting
            var priceTask = GetPriceFromYahoo(stock as string);
            var graphDataTask = GetYahooGraphData(stock as string);

            // await both tasks
            await Task.WhenAll(priceTask, graphDataTask);

            // read the results
            price = priceTask.Result;
            graphData = graphDataTask.Result;
            Console.WriteLine(price);
            Console.WriteLine(graphData);
        }

        async Task<string> GetPriceFromYahoo(string stock)
        {
            var uri = "http://download.finance.yahoo.com/d/quotes.csv?f=snl1c1p2&e=.csv&s=" + stock;
            using (var http = new HttpClient())
            using (var resp = await http.GetAsync(uri))
            {
                return await resp.Content.ReadAsStringAsync();
            }
        }

        async Task<string> GetYahooGraphData(string stock)
        {
            // retrieve the graph data from Yahoo finance
            var uri = $"http://chartapi.finance.yahoo.com/instrument/1.0/{stock}/chartdata;type=quote;range=1d/csv/";
            using (var http = new HttpClient())
            using (var resp = await http.GetAsync(uri))
            {
                return await resp.Content.ReadAsStringAsync();
            }
        }

        public Task<string> GetPrice()
        {
            return Task.FromResult(price);
        }
    }
}
