using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsAPI.Services.Interfaces;
using StudentsAPI.Services.Implementacions;
using StudentsAPI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;

namespace StudentsAPI.Test
{
    [TestClass]
    public class StudentServicetTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //IStudentService service = new StudentService();
        }


        [DataTestMethod]
        [DynamicData(nameof(GetStudents))]
        //[DataRow(0)]
        //[DataRow(1)]
        public void ShouldAddSubjects_Sucess(StudentModel student)
        {
            //Arragne

            //Act
            //Assert
            Assert.IsNotNull(student);

            //// Arrange
            //var mockRepo = new Mock<IStudentService>();
            //mockRepo.Setup(repo => repo.ListAsync())
            //    .ReturnsAsync(GetTestSessions());
            //var controller = new HomeController(mockRepo.Object);

            //// Act
            //var result = await controller.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());

        }


        private IEnumerable<StudentModel> GetStudents()
        {
            return new List<StudentModel>
            {
                new StudentModel{
                    Id = 59,
                    Identification = "123978456",
                    Name = "Name Test",                   
                    Subjects  = new List<SubjectModel>{
                        new SubjectModel{
                            Id = 5,
                            Name = "Testing",
                            TeacherId = 5,
                            Credits = 3                           
                        }
                    }
                }
            };
        }
    }
}
