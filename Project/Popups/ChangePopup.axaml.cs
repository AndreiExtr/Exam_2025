using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Linq;

namespace Project;

public partial class ChangePopup : Window
{
    public ChangePopup()
    {
        InitializeComponent();
    }

    private void CancelOnClick(object sender, RoutedEventArgs e)
    {
        this.Close(false);
    }

    private async void EditOnClick(object sender, RoutedEventArgs e)
    {
        var oldPassword = OldPasswordNameText.Text;
        var newPassword = NewPasswordNameText.Text;
        var newNextPassword = NewNextPasswordNameText.Text;

        if (oldPassword == null || newPassword == null || newNextPassword == null) {
            await ShowMessage("Ошибка", "Заполните пустые поля");
            return;
        }

        if (oldPassword == newPassword || oldPassword == newNextPassword )
        {
            await ShowMessage("Ошибка", "Пароли совпадают");
            return;
        }

        if (newPassword.Length < 6)
        {
            await ShowMessage("Ошибка", "Длина пароли не менее 6 символов");
            return;
        }

        using (var db = new UserItContext())
        {
            var user = db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Password == oldPassword);

            if(user != null)
            {
                user.Password = newPassword;
                db.SaveChanges();
                await ShowMessage("Успешно", "Вы успешно изменили пароль.");

                if (user.RoleId == 3)
                {
                    var clientPage = new ClientPage();
                    clientPage.Show();
                    this.Close(true);

                } else if (user.RoleId == 2)
                {
                    var empPage = new EmployeePage();
                    empPage.Show();
                    this.Close(true);
                }

            }
            else
            {
                await ShowMessage("Ошибка", "Вы неправильно ввели действующий пароль.");
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