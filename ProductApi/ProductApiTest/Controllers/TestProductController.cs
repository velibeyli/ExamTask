using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApi.Controllers;
using ProductApi.Services.Interfaces;
using ProductApiTest.MockData;
using ProductApi.Repositories.Interfaces;
using System.Web.WebPages;
using ProductApi.Services.Implementations;
using ProductApi.Models;

namespace ProductApiTest.Controllers
{
    public class TestProductController
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly ProductService _service;
        public TestProductController(Mock<IProductRepository> repositoryMock, ProductService service)
        {
            _repositoryMock = repositoryMock;
            _service = service;
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnOkStatus()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetAll()).ReturnsAsync(new List<Product>());

            var sut = new ProductController(mockProductService.Object);

            // Act
            var result = await sut.GetAll();

            // Assert
            mockProductService.Verify(x => x.GetAll(), Times.Once());
        }
        public async Task Get_Onsuccess_ReturnListOfProducts()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(x => x.GetAll()).ReturnsAsync(new List<Product>());
            var sut = new ProductController(mockProductService.Object);

            //Act
            var result = await sut.GetAll();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}