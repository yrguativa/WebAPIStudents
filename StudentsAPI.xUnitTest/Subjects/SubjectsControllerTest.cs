using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Controllers;
using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StudentsAPI.xUnitTest.Subjects
{
    public class SubjectsControllerTest
    {
        SubjectsController studentsController;
        ISubjectService _service;
        public SubjectsControllerTest()
        {
            _service = new SubjectServiceFake();
            studentsController = new SubjectsController(_service);
        }

        #region GetAll
        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResultAsync()
        {
            // Act
            var okResult = await studentsController.GetSubjects();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = studentsController.GetSubjects().Result.Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<SubjectModel>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = studentsController.GetSubject(10).Result;

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = 1;

            // Act
            var okResult = studentsController.GetSubject(testGuid).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = 1;

            // Act
            var okResult = studentsController.GetSubject(testGuid).Result.Result as OkObjectResult;

            // Assert
            Assert.IsType<SubjectModel>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as SubjectModel).Id);
        }
        #endregion

        #region Add
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new SubjectModel()
            {
                Credits = 3,
                TeacherId = 1
            };
            studentsController.ModelState.AddModelError(nameof(nameMissingItem.Name), "Required");
            // Act
            var badResponse = studentsController.PostSubject(nameMissingItem).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var testItem = new SubjectModel()
            {
                Credits = 3,
                TeacherId = 1
            };
            // Act
            var okResult = studentsController.PostSubject(testItem).Result.Result;
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseIdItem()
        {
            // Arrange
            var testItem = new SubjectModel()
            {
                Credits = 3,
                TeacherId = 1
            };
            // Act
            var okResult = studentsController.PostSubject(testItem).Result.Result as OkObjectResult;
            var idItem = (int)okResult.Value;
            // Assert           
            Assert.True(idItem > 0);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var nameMissingItem = new SubjectModel()
            {
                Id = 50,
                Credits = 3,
                TeacherId = 1
            };
            // Act
            var notFoundResult = studentsController.PutSubject(nameMissingItem.Id, nameMissingItem).Result;
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequesById()
        {
            // Arrange
            var testItem = new SubjectModel()
            {
                Id = 1,
                Name = "Base de datos",
                Credits = 3,
                TeacherId = 1
            };
            // Act
            var badResponse = studentsController.PutSubject(2, testItem).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new SubjectModel()
            {
                Id = 1,
                Credits = 3,
                TeacherId = 1
            };
            studentsController.ModelState.AddModelError(nameof(nameMissingItem.Name), "Required");
            // Act
            var badResponse = studentsController.PutSubject(nameMissingItem.Id, nameMissingItem).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Update_ValidObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var testItem = new SubjectModel()
            {
                Id = 1,
                Name = "Base de datos",
                Credits = 3,
                TeacherId = 1
            };
            // Act
            var okResult = studentsController.PutSubject(testItem.Id, testItem).Result;
            // Assert
            Assert.IsType<NoContentResult>(okResult);
        }
        #endregion
    }
}
