using System;
using Xunit;
using AutoFixture;
using Moq;
using FluentAssertions;
using ProductApi.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using ProductApi.Services.Implementations;
using ProductApi.Models;

namespace ProductApi.Test.Services.Implementations
{
    public class ProductServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<ILogger<ProductService>> _loggerMock;
        private readonly ProductService _sut;
        public ProductServiceTest()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IProductRepository>>();
            _loggerMock = _fixture.Freeze<Mock<ILogger<ProductService>>>();
            _sut = new ProductService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnData_whenDataFound()
        {
            // Arrange
            var productMock = _fixture.Create<IEnumerable<Product>>();
            _repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(productMock);

            // Act
            var result = await _sut.GetAll().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<Product>>();
            _repositoryMock.Verify(x => x.GetAll(), Times.Once());
        }
    }
}
