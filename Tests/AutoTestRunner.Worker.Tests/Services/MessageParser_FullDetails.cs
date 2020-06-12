using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoTestRunner.Core.Enums;
using AutoTestRunner.Core.Models;
using NUnit.Framework;

namespace AutoTestRunner.Worker.Tests.Services
{
    [TestFixture]
    public class MessageParser_FullDetails
    {

        [Test]
        public void Test()
        {
            var testResultMessage = "X Test_That_Passes[5ms]";
            

            //! Test_That_Ignore[< 1ms]
            //X Test_That_Passes[5ms]
            //V Test_That_Passes_3[1ms]


            else
            {
                Assert.Fail();
            }
            
        }


    }
}
