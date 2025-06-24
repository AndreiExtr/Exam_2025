using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project;

public partial class NewUserPopup : Window
{
    public IEnumerable<string> roleList { get; set; }
    public string SelectedRole { get; set; }
    public NewUserPopup()
    {
        using (var db = new UserItContext())
        {
            roleList = db.Roles
                .Select(x => x.Name)
                .ToList();
            InitializeComponent();
        }
    }

    private async void AddUserOnClick(object sender, RoutedEventArgs e)
    {
        var fNew = firstNew.Text;
        var lNew = lastNew.Text;
        var mNew = middleNew.Text;
        var phNew = phoneNew.Text;
        var emNew = emailNew.Text;
        var logNew = loginNew.Text;
        var passNew = passwordNew.Text;

        if (fNew == null || lNew == null || mNew == null || phNew == null || emNew == null || logNew == null || passNew == null || SelectedRole == null) {
            await ShowMessage("Ошибка", "Заполните пустые поля");
            return;
        }

        using (var db = new UserItContext())
        {
            var exitUser = db.Users.FirstOrDefault(u => u.Login == logNew);
            var role = db.Roles.FirstOrDefault(u => u.Name == SelectedRole);

            if (exitUser != null) {
                await ShowMessage("Ошибка", "Пользователь с таким логином существует");
                return;
            }

            var newUser = new User
            {
                Login = logNew,
                Password = passNew,
                FirstName = fNew,
                LastName = lNew,
                MiddleName = mNew,
                Phone = phNew,
                Email = emNew,
                BlockCount = false,
                RoleId = role.Id
            };

            db.Users.Add(newUser);
            await db.SaveChangesAsync();
            await ShowMessage("Успешно", "Вы успешно добавили нового пользователя");
            this.Close();
        }
    }

    private void CancelOnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
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