using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_RealEstate
{
    // WHERE TO START?
    // 1. Complete the RealEstateType enumeration
    // 2. Complete the RealEstateSale object.  Fill in all properties, then create the constructor.
    // 3. Complete the GetRealEstateSaleList() function.  This is the function that actually reads in the .csv document and extracts a single row from the document and passes it into the RealEstateSale constructor to create a list of RealEstateSale Objects.
    // 4. Start by displaying the the information in the Main() function by creating lambda expressions.  After you have acheived your desired output, then translate your logic into the function for testing.
    class Program
    {
        static void Main(string[] args)
        {
            List<RealEstateSale> realEstateSaleList = GetRealEstateSaleList();
            
            //Display the average square footage of a Condo sold in the city of Sacramento,
            Console.WriteLine(GetAverageSquareFootageByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Condo, "Sacremento"));
            //Use the GetAverageSquareFootageByRealEstateTypeAndCity() function.

            //Display the total sales of all residential homes in Elk Grove.  Use the GetTotalSalesByRealEstateTypeAndCity() function for testing.

            //Display the total number of residential homes sold in the zip code 95842.  Use the GetNumberOfSalesByRealEstateTypeAndZip() function for testing.

            //Display the average sale price of a lot in Sacramento.  Use the GetAverageSalePriceByRealEstateTypeAndCity() function for testing.

            //Display the average price per square foot for a condo in Sacramento. Round to 2 decimal places. Use the GetAveragePricePerSquareFootByRealEstateTypeAndCity() function for testing.

            //Display the number of all sales that were completed on a Wednesday.  Use the GetNumberOfSalesByDayOfWeek() function for testing.


            //Display the average number of bedrooms for a residential home in Sacramento when the 
            // price is greater than 300000.  Round to 2 decimal places.  Use the GetAverageBedsByRealEstateTypeAndCityHigherThanPrice() function for testing.

            //Extra Credit:
            //Display top 5 cities by the number of homes sold (using the GroupBy extension)
            // Use the GetTop5CitiesByNumberOfHomesSold() function for testing.
            Console.ReadKey();
        }

        public static List<RealEstateSale> GetRealEstateSaleList()
        {
            List<RealEstateSale> tempList = new List<RealEstateSale>();
         
            //read in the realestatedata.csv file.  As you process each row, you'll add a new 
            // RealEstateData object to the list for each row of the document, excluding the first.  bool skipFirstLine = true;

                     using (StreamReader reader = new StreamReader("realestatedata.csv"))
            {
                // Get and don't use the first line
                string firstline = reader.ReadLine();
                // Loop through the rest of the lines
                while (!reader.EndOfStream)
                {
                    tempList.Add(new RealEstateSale(reader.ReadLine()));
                }
            }
         
            return tempList;
        }

        public static double GetAverageSquareFootageByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city) 
        {
            //return 0.0;
            return realEstateDataList.Where(x => x.reType == realEstateType && x.City.ToLower() == city.ToLower()).Average(y => y.Sq__ft);
        }

       public static decimal GetTotalSalesByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            return Decimal.Parse(realEstateDataList.Where(x => x.reType == realEstateType && x.City.ToLower() == city.ToLower()).Sum(x => x.Price).ToString());
        }

        public static int GetNumberOfSalesByRealEstateTypeAndZip(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string zipcode)
        {
            return realEstateDataList.Where(x => x.reType == realEstateType && x.Zip.ToString() == zipcode).Count();
        }

        
        public static decimal GetAverageSalePriceByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            //Must round to 2 decimal points
            return (decimal)Math.Round(realEstateDataList.Where(x => x.reType == realEstateType && x.City.ToLower() == city.ToLower()).Average(x => x.Price), 2);
        }
        public static decimal GetAveragePricePerSquareFootByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            //Must round to 2 decimal points
            return (decimal)Math.Round(realEstateDataList.Where(x => x.reType == realEstateType && x.City.ToLower() == city.ToLower()).Average(x => x.Price/x.Sq__ft), 2);
        }

        public static int GetNumberOfSalesByDayOfWeek(List<RealEstateSale> realEstateDataList, DayOfWeek dayOfWeek)
        {
            return realEstateDataList.Count(x => x.Sale_date.DayOfWeek == dayOfWeek);
        }

       public static double GetAverageBedsByRealEstateTypeAndCityHigherThanPrice(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city, decimal price)
        {
            //Must round to 2 decimal points
            return Math.Round(realEstateDataList.Where(x => x.reType == realEstateType && x.City.ToLower() == city && x.Price > price).Average(x => x.Beds), 2);
        }

        public static List<string> GetTop5CitiesByNumberOfHomesSold(List<RealEstateSale> realEstateDataList)
        {
            return realEstateDataList.GroupBy(x => x.City).OrderByDescending(x => x.Count()).Take(5).Select(x => x.Key).ToList();
        }
    }

    public enum RealEstateType
    {
        //fill in with enum types: Residential, MultiFamily, Condo, Lot
        Residential,
        MultiFamily,
        Condo,
        Lot
    }
    class RealEstateSale
    {
        //Create properties, using the correct data types (not all are strings) for all columns of the CSV
        public string Street
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public int Zip
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        public int Beds
        {
            get;
            set;
        }
        public int Baths
        {
            get;
            set;
        }
        public double Sq__ft
        {
            get;
            set;
        }
        public RealEstateType reType
        {
            get;
            set;
        }
        public DateTime Sale_date
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }
        public int Latitude
        {
            get;
            set;
        }
        public int Longitude
        {
            get;
            set;
        }

        //The constructor will take a single string arguement.  This string will be one line of the real estate data.
        // Inside the constructor, you will seperate the values into their corrosponding properties, and do the necessary conversions
        public RealEstateSale(string lineInput)
        {
            string[] inputData = lineInput.Split(',');

            this.Street = inputData[0];
            this.City = inputData[1];
            this.Zip = int.Parse(inputData[2]);
            this.State = inputData[3];
            this.Beds = int.Parse(inputData[4]);
            this.Baths = int.Parse(inputData[5]);
            this.Sq__ft = double.Parse(inputData[6]);
            this.Sale_date = DateTime.Parse(inputData[8]);
            this.Price = int.Parse(inputData[9]);

            switch (inputData[7].ToLower())
            {
                case "residential":
                    this.reType = RealEstateType.Residential;
                    break;

                case "multi-family":
                    this.reType = RealEstateType.MultiFamily;
                    break;

                case "condo":
                    this.reType = RealEstateType.Condo;
                    break;

                case "lot":
                    this.reType = RealEstateType.Lot;
                    break;
            }

            if (Sq__ft == 0)
            {
                this.reType = RealEstateType.Lot;
            }
        }

        //When computing the RealEstateType, if the square footage is 0, then it is of the Lot type, otherwise, use the string
        // value of the "Type" column to determine its corresponding enumeration type.

    }
}
