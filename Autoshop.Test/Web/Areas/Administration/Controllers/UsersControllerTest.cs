namespace Autoshop.Test.Web.Areas.Administration.Controllers
{
    using Autoshop.Models;
    using Autoshop.Test.Mocks;
    using Autoshop.Web.Areas.Admin.Controllers;
    using Autoshop.Web.Areas.Administration.Models.Users;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    using static Autoshop.Common.Constants.CommonConstants;

    public class UsersControllerTest
    {
        private const string FirstUserId = "1";
        private const string FirstUserUsername = "First";
        private const string SecondUserId = "2";
        private const string SecondUserUsername = "Second";
        private const string DefaultErrorMessage = "Invalid identity details.";
        private List<User> users = new List<User>
        {
            new User { Id = FirstUserId, FirstName = FirstUserUsername },
            new User { Id = SecondUserId, FirstName = SecondUserUsername }
        };
        private List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole(Admin),
            new IdentityRole(Writer)
        };

        public UsersControllerTest()
        {
            Configuration.Initialize();
        }

        [Fact]
        public void UsersControllerShouldBeInAdminArea()
        {
            var controller = typeof(UsersController);

            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be(Administration);
        }

        [Fact]
        public void UsersControllerShouldBeOnlyForAdminUsers()
        {
            var controller = typeof(UsersController);

            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be(Admin);
        }

        [Fact]
        public void UsersShouldReturnViewWithValidModel()
        {
            var userManager = this.GetUserManagerMock();
            var controller = new UsersController(userManager.Object, null);

            var result = controller.Users();

            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;

            model.Should().BeOfType<List<UserListingViewModel>>();

            var users = model.As<List<UserListingViewModel>>();

            this.AssertUsersList(users);
        }

        [Fact]
        public async Task UserDetailsWithExistingUserShouldReturnViewWithValidModel()
        {
            var userManager = this.GetUserDetailsManagerMock();
            var roleManager = this.GetRoleManagerMock();

            var controller = new UsersController(userManager.Object, roleManager.Object);

            var result = await controller.UserDetails(string.Empty);
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;

            model.Should().BeOfType<UserDetailsViewModel>();

            var viewModel = model.As<UserDetailsViewModel>();

            var userExpect = this.users.First();
            var userRoleExpect = this.roles.First();
            viewModel.Id.Should().Match(userExpect.Id);
            viewModel.FirstName.Should().Match(userExpect.FirstName);
            viewModel.UserRoles.Count().Should().Be(1);
            viewModel.UserRoles.First().Should().Be(userRoleExpect.Name);
            viewModel.UserRoles.First().Should().Be(userRoleExpect.Name);
            viewModel.Roles.Count().Should().Be(this.roles.Count);
            viewModel.Roles.First().Text.Should().Be(this.roles.First().Name);
            viewModel.Roles.First().Value.Should().Be(this.roles.First().Name);
            viewModel.Roles.Last().Text.Should().Be(this.roles.Last().Name);
            viewModel.Roles.Last().Value.Should().Be(this.roles.Last().Name);
        }

        [Fact]
        public async Task UserDetailsWithUnExistingUserShouldReturnBadRequest()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(default(User));

            var controller = new UsersController(userManager.Object, null);

            var result = await controller.UserDetails(string.Empty);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task AddToRoleWithValidDataShouldReturnViewWithValidModel()
        {
            var user = this.users.First();
            var role = this.roles.First().Name;
            string successMessage = null;
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            userManager
                .Setup(u => u.AddToRoleAsync(user, role))
                .ReturnsAsync(new IdentityResult());
            var roleManager = RoleManagerMock.New;
            roleManager
                .Setup(m => m.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            var controller = new UsersController(userManager.Object, roleManager.Object);
            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);
            controller.TempData = tempData.Object;

            var model = new AddUserToRoleFormModel
            {
                Role = role,
                UserId = user.Id
            };

            var result = await controller.AddToRole(model);

            var assertMsg = $"User {user.UserName} successfully added to the {model.Role} role.";

            this.AssertAddToRoleInvalidRequest(successMessage, model, result, assertMsg);
        }

        [Fact]
        public async Task AddToRoleWithUnExistingUserShouldReturnBadRequest()
        {
            string errorMessage = null;
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(default(User));
            var roleManager = RoleManagerMock.New;
            roleManager
                .Setup(m => m.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            var controller = new UsersController(userManager.Object, roleManager.Object);           
            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[TempDataErrorMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => errorMessage = message as string);
            controller.TempData = tempData.Object;

            var model = new AddUserToRoleFormModel
            {
                Role = string.Empty,
                UserId = string.Empty
            };

            var result = await controller.AddToRole(model);

            this.AssertAddToRoleInvalidRequest(errorMessage, model, result, DefaultErrorMessage);
        }

        [Fact]
        public async Task AddToRoleWithUnExistingRoleShouldReturnBadRequest()
        {
            string errorMessage = null;
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(this.users.First());
            var roleManager = RoleManagerMock.New;
            roleManager
                .Setup(m => m.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            var controller = new UsersController(userManager.Object, roleManager.Object);
            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[TempDataErrorMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => errorMessage = message as string);
            controller.TempData = tempData.Object;

            var model = new AddUserToRoleFormModel
            {
                Role = string.Empty,
                UserId = string.Empty
            };

            var result = await controller.AddToRole(model);

            this.AssertAddToRoleInvalidRequest(errorMessage, model, result, DefaultErrorMessage);
        }

        [Fact]
        public async Task AddToRoleWithInvalidModelStateRoleShouldReturnBadRequest()
        {
            string errorMessage = null;
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(this.users.First());
            var roleManager = RoleManagerMock.New;
            roleManager
                .Setup(m => m.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            var controller = new UsersController(userManager.Object, roleManager.Object);
            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[TempDataErrorMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => errorMessage = message as string);
            controller.TempData = tempData.Object;
            controller.ModelState.AddModelError(string.Empty, string.Empty);
            var model = new AddUserToRoleFormModel
            {
                Role = string.Empty,
                UserId = string.Empty
            };

            var result = await controller.AddToRole(model);

            this.AssertAddToRoleInvalidRequest(errorMessage, model, result, DefaultErrorMessage);
        }

        private void AssertAddToRoleInvalidRequest(string msg, AddUserToRoleFormModel model, IActionResult result, string assertMsg)
        {
            msg.Should().Be(assertMsg);
            result.Should().BeOfType<RedirectToActionResult>();
            var redirect = result.As<RedirectToActionResult>();
            redirect.ActionName.Should().Be("UserDetails");
            redirect.ControllerName.Should().Be("Users");
            redirect.RouteValues.Keys.Should().Contain("area");
            redirect.RouteValues["area"].Should().Be(Administration);
            redirect.RouteValues.Keys.Should().Contain("id");
            redirect.RouteValues["id"].Should().Be(model.UserId);
        }

        private Mock<RoleManager<IdentityRole>> GetRoleManagerMock()
        {
            var roleManager = RoleManagerMock.New;
            roleManager
                .Setup(m => m.Roles)
                .Returns(this.roles.AsQueryable());

            return roleManager;
        }

        private Mock<UserManager<User>> GetUserDetailsManagerMock()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(this.users.First());

            userManager
                .Setup(u => u.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<string>
                {
                    this.roles.First().Name
                });

            return userManager;
        }

        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.Users)
                .Returns((this.users).AsQueryable());

            return userManager;
        }

        private void AssertUsersList(List<UserListingViewModel> users)
        {
            users.Should().Match(items => items.Count() == 2);
            users.First().Should().Match(u => u.As<UserListingViewModel>().Id == FirstUserId);
            users.First().Should().Match(u => u.As<UserListingViewModel>().FirstName == FirstUserUsername);
            users.Last().Should().Match(u => u.As<UserListingViewModel>().Id == SecondUserId);
            users.Last().Should().Match(u => u.As<UserListingViewModel>().FirstName == SecondUserUsername);
        }
    }
}
