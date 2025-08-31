using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Primitives;
using Moq;
using WebAppCoreMVC.Areas.ADMIN.Controllers;
using WebAppCoreMVC.Models;
using WebAppCoreMVC.Services;

namespace WebAppKondi.Tests
{
    public class AboutControllerTests
    {

        [Fact]
        public void Create_About_RetourneVue()
        {
            // Arrange
            var mockService = new Mock<IAboutService>();
            var controller = new AboutController(mockService.Object);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("create", result.ViewName);
        }

        [Fact]
        public void Create_AboutModelStateInvalide_RetourneVue()
        {
            // Arrange
            var mockService = new Mock<IAboutService>();
            var controller = new AboutController(mockService.Object);
            var formData = new Dictionary<string, StringValues>
            {
                { "Libelle", "" },
                { "Description", "" },
                { "Icone", "" }
            };

            IFormCollection collection = new FormCollection(formData);

            controller.ModelState.AddModelError("Libelle", "Libelle obligatoire");
            controller.ModelState.AddModelError("Description", "Description obligatoire");
            controller.ModelState.AddModelError("Icone", "Icone obligatoire");

            // Act
            var result = controller.Create(collection) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal("create", result.ViewName);
        }

        [Fact]
        public void Create_AboutModelStateIsValide_RedirecteIndex()
        {
            // Arrange
            var mockService = new Mock<IAboutService>();
            var controller = new AboutController(mockService.Object);
            var about = new About { Description = "Description", Icone = "Icone", Libelle = "Libelle" };
            var formData = new Dictionary<string, StringValues>
            {
                { "Libelle", about.Libelle },
                { "Description", about.Description },
                { "Icone", about.Icone }
            };

            IFormCollection collection = new FormCollection(formData);

            // Simuler TempData
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, new FakeTempDataProvider());
            controller.TempData = tempData;

            // On simule que ModelState est valide
            controller.ModelState.Clear();

            // Act
            var result = controller.Create(collection) as RedirectToRouteResult;
            // Assert
            mockService.Verify(s => s.EnregistrerElement(It.Is<About>(a =>
            a.Libelle == "Libelle" && a.Description == "Description" && a.Icone == "Icone")), Times.Once);

            Assert.True(controller.TempData.ContainsKey("Message"));
            Assert.Equal("Nouvel enrégistrement réussie avec succès !", controller.TempData["Message"]);
        }

        private class FakeTempDataProvider : ITempDataProvider
        {
            private Dictionary<string, object> _store = new();
            public IDictionary<string, object> LoadTempData(HttpContext context)
            {
                return _store;
            }

            public void SaveTempData(HttpContext context, IDictionary<string, object> values)
            {
                _store = new Dictionary<string, object>(values);
            }
        }

        [Fact]
        public void Create_About_RetourneException()
        {
            // Arrange
            var mockService = new Mock<IAboutService>();
            var controller = new AboutController(mockService.Object);
            var about = new About();
            var exception = new Exception("erreur");
            mockService.Setup(s => s.EnregistrerElement(It.IsAny<About>())).Throws(exception);
            var formData = new Dictionary<string, StringValues>
            {
                
            };
            IFormCollection collection = new FormCollection(formData);

            // TempData simulée
            var httpContext = new DefaultHttpContext();
            controller.TempData = new TempDataDictionary(httpContext, new FakeTempDataProvider());

            //Act & Assert
            var ex = Assert.Throws<Exception>(() => controller.Create(collection));
            Assert.Equal("erreur", ex.Message);

            // Et on peut aussi vérifier que TempData["Message"] n'est pas défini
            Assert.False(controller.TempData.ContainsKey("Message"));
        }
    }
}