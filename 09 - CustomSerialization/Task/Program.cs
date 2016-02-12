using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Task.DB;
using Task.TestHelpers;

namespace Task
{
    class Program
    {
        static Northwind dbContext;

        static void Main(string[] args)
        {
            Initialize();
            SerializationCallbacks();
            Console.ReadKey();
        }

        public static void Initialize()
        {
            dbContext = new Northwind();
        }

        public static void SerializationCallbacks()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            //adding DbContext to StreamingContext
            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, dbContext)), true);
            var categories = dbContext.Categories.ToList();

            tester.SerializeAndDeserialize(categories);
        }

        public static void ISerializable()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
            var products = dbContext.Products.ToList();

            tester.SerializeAndDeserialize(products);
        }


        public static void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(), true);
            var orderDetails = dbContext.Order_Details.ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }

        public static void IDataContractSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(new DataContractSerializer(typeof(IEnumerable<Order>)), true);
            var orders = dbContext.Orders.ToList();

            tester.SerializeAndDeserialize(orders);
        }

        //public static T UnProxy<T>(DbContext context, T proxyObject) where T : class
        //{
        //    var proxyCreationEnabled = context.Configuration.ProxyCreationEnabled;
        //    try
        //    {
        //        context.Configuration.ProxyCreationEnabled = false;
        //        T poco = context.Entry(proxyObject).CurrentValues.ToObject() as T;
        //        return poco;
        //    }
        //    finally
        //    {
        //        context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        //    }
        //}
    }
}
