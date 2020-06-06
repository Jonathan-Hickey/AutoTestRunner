﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTestRunner.Core.Repositories.Implementation;
using AutoTestRunner.Core.Services.Implementation;
using AutoTestRunner.Core.Tests.Fakes;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AutoTestRunner.Core.Tests.Repositories
{
    [TestFixture]
    public class FileRepository_GetAllAsync_Tests
    {
        [Test]
        public async Task When_NonNullObject_Passed_Should_Then_Add_ToFile()
        {
            var moqFileHelper = new Mock<IFileHelper>();

            var list = new List<string>();
            list.Add("{\"FakeData\":\"This is some fake input\"}");
            list.Add("{\"FakeData\":\"This is some fake input\"}");
            list.Add("{\"FakeData\":\"This is some fake input\"}");

            moqFileHelper.Setup(f => f.ReadFileAsync()).ReturnsAsync(list);

            var fileRepository = new FileRepository<FakeNullableData>(new JsonService(), moqFileHelper.Object);

            var results = await fileRepository.GetAllAsync();

            var expectedResults = new List<FakeNullableData>
            {
                new FakeNullableData {FakeData = "This is some fake input"},
                new FakeNullableData {FakeData = "This is some fake input"},
                new FakeNullableData {FakeData = "This is some fake input"},
            };

            moqFileHelper.Verify(f => f.ReadFileAsync(), Times.Once(), "Write was not called");

            results.Should().BeEquivalentTo(expectedResults);
        }
    }
}
