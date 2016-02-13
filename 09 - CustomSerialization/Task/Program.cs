using CustomSerialization.Task.DB;
using CustomSerialization.Task.SerializationSurrogates;
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
            IDataContractSurrogate();
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

            //adding DbContext to StreamingContext
            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, dbContext)), true);
            var products = dbContext.Products.ToList();
            tester.SerializeAndDeserialize(products);
        }

        public static void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            //using surrogates
            //DataContractSurrogate used instead of SerializationSurrogate because 
            //it is not compatible with DataCOntractSerializer
            var orderDetailsSurrogated = new Order_DetailsSurrogated() { DbContext = dbContext };

            var serializer = new DataContractSerializer(
                    typeof(Product),
                    new Type[] { typeof(List<Order_Detail>), typeof(Order_Detail) },
                    int.MaxValue,
                    false,
                    true,
                    orderDetailsSurrogated
                    );

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(serializer);
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

        
    }
}
