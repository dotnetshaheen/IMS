using Application.Abstractions.Repositories.Base;
using Application.Features.TestFeature;
using Domain.Entities;
using Infrastructure.Context;
using MockQueryable.Moq;
using Moq;
using static Application.Features.TestFeature.GetStaticDataQuery;

namespace UnitTest
{
    public class UnitTest1
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        public UnitTest1()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void Test1()

        {
            IEnumerable<User> users = new List<User>
            {
                new User { Id = 1, LoginName = "Test" }
            };
            var userMock = users.BuildMock<User>();

            _unitOfWorkMock.Setup(x => x.User.Queryable).Returns(userMock);

            var query = new GetStaticDataQuery();
            var handler = new GetStaticDateQueryHandler(_unitOfWorkMock.Object);

            var res = handler.Handle(query, default).GetAwaiter().GetResult();

            Assert.Equal("Test", res);

        }
    }
}