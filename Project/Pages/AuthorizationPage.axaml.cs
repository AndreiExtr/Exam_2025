using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project
{
    public partial class AuthorizationPage : Window
    {
        private int loginAttempts = 3;
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private async void LoginOnClick(object sender, RoutedEventArgs e)
        {
            var login = LoginNameText.Text;
            var password = PasswordNameText.Text;

            if (login == null || password == null) {
                await ShowMessage("Ошибка", "Заполните пустые поля");
                return;
            }

            using (var db = new UserItContext())
            {
                var user = await db.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == login);

                if (user != null)
                {
                    if (user.BlockCount)
                    {
                        await ShowMessage("Ошибка", "Вы заблокированы. Обратитесь к администратору.");
                        return;
                    }

                    if (user.Password == password) {
                        await ShowMessage("Успешно", "Вы успешно авторизовались.");

                        if(user.RoleId == 2 || user.RoleId == 3)
                        {
                            var changePopup = new ChangePopup();
                            var result = await changePopup.ShowDialog<bool>(this);
                            if (result) {
                                this.Close();
                            }                      
                        }
                        else
                        {
                            var adminPage = new AdministratorPage(user);
                            adminPage.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        loginAttempts--;
                        if (loginAttempts <= 0) { 
                            user.BlockCount = true;
                            await db.SaveChangesAsync();
                            await ShowMessage("Ошибка", "Вы заблокированы после 3 неудачных попыток");
                        }
                        else
                        {
                            await ShowMessage("Ошибка", $"Вы неправильно ввели логин или пароль. Осталось попыток: {loginAttempts}");
                            return;
                        }
                    }
                }
                else
                {
                    await ShowMessage("Ошибка", "Вы неправильно ввели логин или пароль.");
                    return;
                }
            }
        }

        private async System.Threading.Tasks.Task ShowMessage(string title, string message)
        {
            var dialog = new Window
            {
                Title = title,
                Content = message,
                SizeToContent = SizeToContent.WidthAndHeight,
                Padding = new Avalonia.Thickness(40),
                MinWidth = 300
            };

            await dialog.ShowDialog(this);
        }
    }
}