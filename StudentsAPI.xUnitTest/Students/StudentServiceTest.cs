using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Controllers;
using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StudentsAPI.xUnitTest.Students
{
    public class StudentServiceTest
    {
        StudentsController studentsController;
        IStudentService _service;
        public StudentServiceTest()
        {
            _service = new StudentServiceFake();
            studentsController = new StudentsController(_service);
        }
        #region GetAll
        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResultAsync()
        {
            // Act
            var okResult = await studentsController.GetStudents();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = studentsController.GetStudents().Result.Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<StudentModel>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = studentsController.GetStudent(10).Result;

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = 1;

            // Act
            var okResult = studentsController.GetStudent(testGuid).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = 1;

            // Act
            var okResult = studentsController.GetStudent(testGuid).Result.Result as OkObjectResult;

            // Assert
            Assert.IsType<StudentModel>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as StudentModel).Id);
        }
        #endregion
    }
}
