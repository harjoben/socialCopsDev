﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.CoreService;
using System.ServiceModel;
using ConsoleTest.Models;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

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
               // Console.ReadLine();
                
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

      
        public void SaveUser()
        {
            logger.warning("Enter TestConnection Method");
            try
            {
                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                ConsoleTest.Models.userItem item = new ConsoleTest.Models.userItem();
                item.userName = "Varun Banka";
                item.userAddress = "scdscscsdac";
                item.email = "bankav@gmail.com";
                item.date = DateTime.Now;

                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ConsoleTest.Models.userItem));
                serializer.WriteObject(stream, item);
                byte[] data = client.UploadData("http://127.0.0.1/Core.svc/SaveUser", "POST", stream.ToArray());
                stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(bool));
                bool result = (bool)serializer.ReadObject(stream);
                Console.WriteLine("Save User Result : " + result);
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
           // test.TestConnection();
            test.SaveUser();

        }
    }
}

