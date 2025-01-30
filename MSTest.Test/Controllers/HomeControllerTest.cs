using Telerik.JustMock;
using MvcUnitTesting_dotnet8.Models;
using MvcUnitTesting_dotnet8.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace MvcUnitTesting.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Returns_All_books_In_DB()
        {
            //Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            Mock.Arrange( () => bookRepository.GetAll()).
                Returns(new List<Book>()
                {
                    new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                    new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
                    new Book { Genre="Science Fiction", ID=1, Name="Escape from the vortex", Price=12.50m},
                    new Book { Genre="History", ID=2, Name="The Battle of the Somme", Price=22m},
                }).MustBeCalled();

            //Act
            HomeController controller = new HomeController(bookRepository,null);
            ViewResult viewResult = controller.Index(null) as ViewResult;
            var model = viewResult.Model as IEnumerable<Book>;

            //Assert
            Assert.AreEqual(4, model.Count());

        }

       
        [TestMethod]
        public void Privacy()
        {
            // Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            HomeController controller = new HomeController(bookRepository,null);

            // Act
            ViewResult result = controller.Privacy() as ViewResult;

            // Assert
            Assert.AreEqual("Your Privacy is our concern", result.ViewData["Message"]);
        }

        // Failing Test
        [TestMethod]
        public void Index_Should_Set_ViewData_Genre()
        {
            // Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            HomeController controller = new HomeController(bookRepository, null);

            // Act
            ViewResult viewResult = controller.Index("Fiction") as ViewResult;

            // Assert - Expect ViewData to contain the "Genre" key
            Assert.IsTrue(viewResult.ViewData.ContainsKey("Genre"), "ViewData does not contain 'Genre'");
            Assert.AreEqual("Fiction", viewResult.ViewData["Genre"], "Genre value is incorrect");
        }

    }
}
