using NUnit.Framework;
using System.Diagnostics;

namespace AutoTestRunner.Worker.Tests
{
    [TestFixture]
    public class StartProcess_Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Open_CMD()
        {
            using (Process myProcess = new Process())
            {
                ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
                info.ArgumentList.Add("cd C:\\Users\\Jonathan\\source\\repos\\TestProjectUsedByAutoTestRunner\\TestProjectUsedByAutoTestRunner");
                info.ArgumentList.Add("dotnet test");
                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                myProcess.StartInfo = info;
                myProcess.Start();

                Debug.WriteLine(myProcess.Id);


                //myProcess.StartInfo.UseShellExecute = false;
                //// You can start any process, HelloWorld is a do-nothing example.
                //myProcess.StartInfo.FileName = "C:\\HelloWorld.exe";
                //myProcess.StartInfo.CreateNoWindow = true;
                //myProcess.Start();

                //myProcess.StartInfo.UseShellExecute = false;
                // You can start any process, HelloWorld is a do-nothing example.
                //myProcess.StartInfo.FileName = "C:\\HelloWorld.exe";

                //myProcess.Start();
                // This code assumes the process you are starting will terminate itself.
                // Given that is is started without a window so you cannot terminate it
                // on the desktop, it must terminate itself or you can do it programmatically
                // from this application using the Kill method.

            }

            Assert.Pass();
        }
    }
}