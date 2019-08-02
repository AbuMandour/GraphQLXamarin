using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GraphQLDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphQLWithHttpClientPage : BaseContentPage
    {
        public GraphQLWithHttpClientPage()
        {
            InitializeComponent();
        }
    }
}