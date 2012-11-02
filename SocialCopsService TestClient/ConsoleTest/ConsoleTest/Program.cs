using System;
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





        //public void TestConnection()
        //{

        //    logger.warning("Enter TestConnection Method");
        //    try
        //    {
        //        client = new CoreClient();
        //        TestConnectionRequest request = new TestConnectionRequest();
        //        TestConnectionResponse response;
        //        response = client.TestConnection(request);
        //        Console.WriteLine("Testing Connection Result : " + response.TestConnectionResult);
        //        logger.warning("Exit TestConnection Method");
        //        Console.ReadLine();

        //    }
        //    catch (FaultException<ConsoleTest.CoreService.Bug> Fex)
        //    {
        //        logger.error("ErrorMessage::" + Fex.Detail.ErrorMessage + Environment.NewLine);
        //        logger.error("ErrorDetails::" + Environment.NewLine + Fex.Detail.ErrorDetails);
        //        Console.ReadLine();
        //    }
        //    finally
        //    {
        //        if (null != client)
        //        {
        //            client.Close();
        //        }
        //    }

        //}

      
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
                item.email = "bankfffafffv@gmail.com";
                item.date = DateTime.Now;

                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ConsoleTest.Models.userItem));
                serializer.WriteObject(stream, item);
                byte[] data = client.UploadData("http://127.0.0.1/Core.svc/SaveUser", "POST", stream.ToArray());
                stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(int));
                int result = (int)serializer.ReadObject(stream);
                Console.WriteLine("Save User Result : " + result);
                logger.warning("Exit TestConnection Method");
                Console.ReadLine();
            }
            catch (FaultException<ConsoleTest.CoreService.Bug> Fex)
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


        public void GetUsers()
        {
            WebClient client = new WebClient();
            byte[] data = client.DownloadData("http://127.0.0.1/Core.svc/GetUsers");
            MemoryStream stream = new MemoryStream(data);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ConsoleTest.Models.userItem[]));
            ConsoleTest.Models.userItem[] user = (ConsoleTest.Models.userItem[])serializer.ReadObject(stream);
            foreach (ConsoleTest.Models.userItem temp in user)
            {
                Console.WriteLine(temp.userName);
                Console.ReadLine();
            }
        }

        public void GetComplaints()
        {
            WebClient client = new WebClient();
            byte[] data = client.DownloadData("http://127.0.0.1/Core.svc/GetComplaintsByCategory/pothole");
            MemoryStream stream = new MemoryStream(data);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ConsoleTest.Models.complaintItem[]));
            ConsoleTest.Models.complaintItem[] user = (ConsoleTest.Models.complaintItem[])serializer.ReadObject(stream);
            foreach (ConsoleTest.Models.complaintItem item in user)
            {
                Console.WriteLine(item.title);
            }
 
           
        }

        public void SaveComplaint()
        {
            try
            {
            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
                ConsoleTest.Models.complaintItem temp = new ConsoleTest.Models.complaintItem();
                //temp.complaintId = complaint.complaintId;
                temp.userId = 1;
                temp.title = "Rubbish Test";
                temp.details = "Test Rubbish";
                temp.numLikes = 2;
                temp.numComments = 1;
                temp.picture = "zascac";
                temp.complaintDate = DateTime.Now;
                temp.location = "dasdas";
                temp.latitude = (float)0.323;
                temp.longitude = (float)32.44;
                temp.category = "pothole";
                temp.complaintStatus = "Solved";
                temp.date = DateTime.Now;
                temp.isAnonymous = 1;
                string path = @"C:\Users\Banka\Desktop\images.jpg";
                FileStream fs=new FileStream(path,FileMode.Open);
                byte[] PhotoBytes = new byte[fs.Length];
                fs.Read(PhotoBytes, 0, PhotoBytes.Length);
                temp.ImageByte = PhotoBytes;
                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ConsoleTest.Models.complaintItem));
                serializer.WriteObject(stream, temp);
                byte[] data = client.UploadData("http://127.0.0.1/Core.svc/SaveComplaint", "POST", stream.ToArray());
                stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(int));
                int result = (int)serializer.ReadObject(stream);
                Console.WriteLine("Save User Result : " + result);
                logger.warning("Exit TestConnection Method");
                Console.ReadLine();
            }
            catch (FaultException<ConsoleTest.CoreService.Bug> Fex)
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
            test.GetComplaints();
            test.GetComplaints();
                 //test.GetUsers();
           // test.SaveComplaint();
            Console.ReadLine();
        }
    }
}

