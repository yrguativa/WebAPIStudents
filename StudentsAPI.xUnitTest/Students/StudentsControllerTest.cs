﻿using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Controllers;
using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StudentsAPI.xUnitTest.Students
{
    public class StudentsControllerTest
    {
        StudentsController studentsController;
        IStudentService _service;
        public StudentsControllerTest()
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

        #region Add
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new StudentModel()
            {
                Identification = "897231"
            };
            studentsController.ModelState.AddModelError(nameof(nameMissingItem.Name), "Required");
            // Act
            var badResponse = studentsController.PostStudent(nameMissingItem).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var testItem = new StudentModel()
            {
                Name = "Julian Romero",
                Identification = "568974"
            };
            // Act
            var okResult = studentsController.PostStudent(testItem).Result.Result;
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseIdItem()
        {
            // Arrange
            var testItem = new StudentModel()
            {
                Name = "Diana Carolina Triana",
                Identification = "369852"
            };
            // Act
            var okResult = studentsController.PostStudent(testItem).Result.Result as OkObjectResult;
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
            var nameMissingItem = new StudentModel()
            {
                Id = 50,
                Identification = "897231"
            };
            // Act
            var notFoundResult = studentsController.PutStudent(nameMissingItem.Id, nameMissingItem).Result;
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequesById()
        {
            // Arrange
            var testItem = new StudentModel()
            {
                Id = 1,
                Name = "July Jimenez",
                Identification = "897231"
            };
            // Act
            var badResponse = studentsController.PutStudent(2, testItem).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new StudentModel()
            {
                Id = 1,
                Identification = "897231"
            };
            studentsController.ModelState.AddModelError( nameof(nameMissingItem.Name), "Required");
            // Act
            var badResponse = studentsController.PutStudent(nameMissingItem.Id, nameMissingItem).Result;
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Update_ValidObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var testItem = new StudentModel()
            {
                Id = 1,
                Name = "Julian Romero",
                Identification = "568974"
            };
            // Act
            var okResult = studentsController.PutStudent(testItem.Id, testItem).Result;
            // Assert
            Assert.IsType<NoContentResult>(okResult);
        }
        #endregion
    }
}
