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
        public void Index_Returns_All_Books_In_DB()
        {
            // Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            Mock.Arrange(() => bookRepository.GetAll()).Returns(new List<Book>()
    {
        new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
        new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
        new Book { Genre="Science Fiction", ID=3, Name="Escape from the Vortex", Price=12.50m},
        new Book { Genre="History", ID=4, Name="The Battle of the Somme", Price=22m}
    });

            HomeController controller = new HomeController(bookRepository, null);

            // Act: Call Index with null (to get all books)
            ViewResult viewResult = controller.Index(null) as ViewResult;
            var model = viewResult.Model as IEnumerable<Book>;

            // Assert: Should return all 4 books
            Assert.IsNotNull(model, "Model should not be null");
            Assert.AreEqual(4, model.Count(), "Should return all books in DB");
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

        // Lab 2 second Test
        [TestMethod]
        public void test_book_by_genre()
        {
            // Mocking the repo
            var bookRepository = Mock.Create<IRepository<Book>>();

            // Simulate books with different genres
            Mock.Arrange(() => bookRepository.GetAll()).Returns(new List<Book>()
            {
                new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
                new Book { Genre="Science Fiction", ID=3, Name="Escape from the Vortex", Price=12.50m},
                new Book { Genre="History", ID=4, Name="The Battle of the Somme", Price=22m}
            });

            HomeController controller = new HomeController(bookRepository, null);

            // Act: Call Index action with "Fiction" genre
            ViewResult viewResult = controller.Index("Fiction") as ViewResult;
            var model = viewResult.Model as List<Book>;

            // Assert: Expect only 2 Fiction books
            Assert.IsNotNull(model, "Model should not be null");
            Assert.AreEqual(2, model.Count, "Should return only 2 Fiction books");
            Assert.IsTrue(model.All(b => b.Genre == "Fiction"), "All books should be Fiction");
        }


    }
}
