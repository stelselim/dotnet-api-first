using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
/// Third party library
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace question3
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();



        static async Task Main()
        {
            /// Comment one, Uncomment the other!

            /// This is question 3 -  A

            /// UNCOMMENT  line 26
            // await question3a();



            /// This is question 3 - B

            /// UNCOMMENT line 33
             await question3b();

        }



        static async Task question3a()
        {

            Console.WriteLine("Enter Latitude");
            var Lat = Console.ReadLine();

            Console.WriteLine("Enter Longitude");
            var Lon = Console.ReadLine();


            var LAT = Double.Parse(Lat);
            var LON = Double.Parse(Lon);

            JObject local = await getRequestFor3A(LAT, LON);


            if (local == null)
            {
                Console.WriteLine("An Error Occured");

            }
            else
            {
                var x = local["product"];
                var y = local["init"];
                var z = local["dataseries"];

                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(z);

            }

        }




        /// This function works for Question 3 - A
        static async Task<JObject> getRequestFor3A(double LAT, double LON)
        {
            string url = "http://www.7timer.info/bin/astro.php?lon=" + LON.ToString() + "&lat=" + LAT.ToString() + "&ac=0&unit=metric&output=json&tzshift=0";

            try
            {

                string responseBody = await client.GetStringAsync(url);

                JObject o = JObject.Parse(responseBody);
                // Console.WriteLine(o);

                return o;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }

        }


        /// This is complete of Question 3 - B
        static async Task question3b()
        {

            Console.WriteLine("It's known that Currency is not the best thing to check for Turkey. But Enjoy For TRY!");
            Console.WriteLine("This is a currency calculator with European Central Bank Api.");

            Console.WriteLine("Choose what to do?");
            Console.WriteLine("\t 1-* Convert TRY to USD, EUR");
            Console.WriteLine("\t 2-* Check The Latest Currencies for TRY=1");
            Console.WriteLine("\t 3-* Check The TRY at specific date! (Since 2000)");
            Console.WriteLine("Enter: 1,2 or 3!");


            var operation = Console.ReadLine();
            if (operation == "1")
            {

                await operation1();
            }
            if (operation == "2")
            {
                await operation2();
            }
            if (operation == "3")
            {
                await operation3();
            }



        }



        static async Task operation1()
        {

            Console.WriteLine("Enter a amount of Turkish Lira, Ex:200 or 200.5");
            var read = Console.ReadLine();
            double amount = double.Parse(read);


            string url = "https://api.exchangeratesapi.io/latest?base=TRY";
            try
            {

                string responseBody = await client.GetStringAsync(url);

                JObject o = JObject.Parse(responseBody);
                var usdCoeff = o["rates"]["USD"];
                var euroCoeff = o["rates"]["EUR"];

                var eur = euroCoeff.ToObject<double>() * amount;
                var usd = usdCoeff.ToObject<double>() * amount;

                Console.WriteLine("\n\nConverted!\n");
                Console.WriteLine("TRY: " + amount.ToString());
                Console.WriteLine("USD: " + usd.ToString());
                Console.WriteLine("EUR: " + eur.ToString());
                Console.WriteLine("");

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }

        static async Task operation2()

        {
            string url = "https://api.exchangeratesapi.io/latest?base=TRY";
            try
            {

                string responseBody = await client.GetStringAsync(url);

                JObject o = JObject.Parse(responseBody);
                var dat = o["rates"];
                Console.WriteLine(dat);

                var usdCoeff = o["rates"]["USD"];
                var euroCoeff = o["rates"]["EUR"];

                var eur = 1.0 / euroCoeff.ToObject<double>();
                var usd = 1.0 / usdCoeff.ToObject<double>();

                Console.WriteLine("\nAlso, you can see the 1 Euro, 1 American Dollar to TRY");
                Console.WriteLine("1 EURO => " + eur);
                Console.WriteLine("1 USD => " + usd);



            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }
        static async Task operation3()

        {

            Console.WriteLine("Enter a date, Ex: 2010-02-15");
            var read = Console.ReadLine();


            string url = "https://api.exchangeratesapi.io/" + read + "?base=TRY";
            try
            {

                string responseBody = await client.GetStringAsync(url);

                JObject o = JObject.Parse(responseBody);
                var dat = o["rates"];
                Console.WriteLine(dat);


                var usdCoeff = o["rates"]["USD"];
                var euroCoeff = o["rates"]["EUR"];

                var eur = 1.0 / euroCoeff.ToObject<double>();
                var usd = 1.0 / usdCoeff.ToObject<double>();

                Console.WriteLine("\nAlso, you can see the 1 Euro, 1 American Dollar to TRY");
                Console.WriteLine("1 EURO => " + eur);
                Console.WriteLine("1 USD => " + usd);



            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }



    }



}
