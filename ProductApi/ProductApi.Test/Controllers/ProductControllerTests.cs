using System;
using Xunit;
using AutoFixture;
using Moq;
using FluentAssertions;
using ProductApi.Services.Interfaces;
using ProductApi.Controllers;
using System.Threading.Tasks;
using ProductApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Test.NewFolder
{
    public class ProductControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProductService> _serviceMock;
        private readonly ProductController _sut;

        public ProductControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IProductService>>();
            _sut = new ProductController(_serviceMock.Object); // creates the implementation in-memory
        }
        [Fact]
        public async Task GetProducts_ShouldReturnOkResponse()
        {
            // Arrange
            var productMock = _fixture.Create<IEnumerable<Product>>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(productMock);

            // Act
            var result = await _sut.GetAll().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Product>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(productMock.GetType());
            _serviceMock.Verify(x => x.GetAll(), Times.Once());
        }
        [Fact]
        public async Task GetProductById_ShouldReturnOkResponse()
        {
            // Arrange
            var productMock = _fixture.Create<Product>();
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.GetById(id)).ReturnsAsync(productMock);

            // Act
            var result = await _sut.GetById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Product>>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(productMock.GetType());
            _serviceMock.Verify(x => x.GetById(id), Times.Once());
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNotFound()
        {
            // Arrange
            Product response = null;
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.GetById(id)).ReturnsAsync(response);

            // Act
            var result = await _sut.GetById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
            _serviceMock.Verify(x => x.GetById(id), Times.Once());
        }

        [Fact]
        public async Task GetProductById_ShouldReturnBadRequest_WhenInputisEqualZero()
        {
            // Arrange
            var response = _fixture.Create<Product>();
            int id = 0;
            _serviceMock.Setup(x => x.GetById(id)).ReturnsAsync(response);

            // Act
            var result = await _sut.GetById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.GetById(id), Times.Never());
        }

        [Fact]
        public async Task GetProductById_ShouldReturnBadRequest_WhenInputisLessThanZero()
        {
            // Arrange
            var response = _fixture.Create<Product>();
            int id = -1;
            _serviceMock.Setup(x => x.GetById(id)).ReturnsAsync(response);

            // Act
            var result = await _sut.GetById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.GetById(id), Times.Never());
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arrange
            var request = _fixture.Create<Product>();
            var response = _fixture.Create<Product>();
            _serviceMock.Setup(x => x.CreateProduct(request)).ReturnsAsync(response);

            // Act
            var result = await _sut.Create(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Product>>();
            result.Result.Should().BeAssignableTo<CreatedAtRouteResult>();
            _serviceMock.Verify(x => x.CreateProduct(response), Times.Never());

        }

        [Fact]
        public async Task CreateProduct_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            //Arrange
            var request = _fixture.Create<Product>();
            _sut.ModelState.AddModelError("Subject","The subject field is requierd.");
            var response = _fixture.Create<Product>();
            _serviceMock.Setup(x => x.CreateProduct(request)).ReturnsAsync(response);

            // Act
            var result = await _sut.Create(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<CreatedAtRouteResult>();
            _serviceMock.Verify(x => x.CreateProduct(response), Times.Never());

        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenDeletedRecord()
        {
            //Arrange
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.DeleteProductById(id));

            // Act
            var result = await _sut.Delete(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();

        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNotFound_WhenRecordNotFound()
        {
            //Arrange
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.DeleteProductById(id));

            // Act
            var result = await _sut.Delete(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();

        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnBadRequest_WhenInputIsZero()
        {
            //Arrange
            var id = 0;
            _serviceMock.Setup(x => x.DeleteProductById(id));

            // Act
            var result = await _sut.Delete(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.DeleteProductById(id), Times.Never());

        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnBadRequest_WhenInputIsZero()
        {
            //Arrange
            var id = 0;
            var request = _fixture.Create<Product>();
            _serviceMock.Setup(x => x.UpdateProduct(id,request));

            // Act
            var result = await _sut.Update(id,request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.UpdateProduct(id,request), Times.Never());

        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            //Arrange
            var id = _fixture.Create<int>();
            var request = _fixture.Create<Product>();
            _sut.ModelState.AddModelError("Subject", "The subject field is reuired.");
            var response = _fixture.Create<Product>();
            _serviceMock.Setup(x => x.UpdateProduct(id, request));

            // Act
            var result = await _sut.Update(id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.UpdateProduct(id, request), Times.Never());

        }
        [Fact]
        public async Task UpdateProduct_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arrange
            var id = _fixture.Create<int>();
            var request = _fixture.Create<Product>();
            _sut.ModelState.AddModelError("Subject", "The subject field is reuired.");
            var response = _fixture.Create<Product>();
            _serviceMock.Setup(x => x.UpdateProduct(id, request));

            // Act
            var result = await _sut.Update(id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.UpdateProduct(id, request), Times.Never());

        }
        [Fact]
        public async Task UpdateProduct_ShouldReturnNotFound_WhenRecordNotFound()
        {
            //Arrange
            var id = _fixture.Create<int>();
            var request = _fixture.Create<Product>();
            _serviceMock.Setup(x => x.UpdateProduct(id, request));

            // Act
            var result = await _sut.Update(id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMock.Verify(x => x.UpdateProduct(id, request), Times.Never());

        }
    }
}