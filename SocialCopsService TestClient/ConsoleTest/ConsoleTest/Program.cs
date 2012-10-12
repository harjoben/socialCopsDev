using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.CoreService;
using System.ServiceModel;

namespace ConsoleTest
{
    class Program
    {
        CoreClient client = null;
        Logger logger = new Logger();

        public void TestConnection()
        {

            logger.warning("Enter TestConnection Method");
            try
            {
                client = new CoreClient();
                TestConnectionRequest request = new TestConnectionRequest();
                TestConnectionResponse response;
                response = client.TestConnection(request);
                Console.WriteLine("Testing Connection Result : " + response.TestConnectionResult);
                logger.warning("Exit TestConnection Method");
                Console.ReadLine();
                
            }
            catch (FaultException<Bug> Fex)
            {
                logger.error("ErrorMessage::" + Fex.Detail.ErrorMessage + Environment.NewLine);
                logger.error("ErrorDetails::" + Environment.NewLine + Fex.Detail.ErrorDetails);
                Console.ReadLine();
            }
            finally
            {
                if (null != client)
                {
                    client.Close();
                }
            }

        }



        static void Main(string[] args)
        {
            Program test = new Program();
            test.logger.start("Social Cops Service Test Client");
            test.TestConnection();
        }
    }
}

