using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouthProtectionAplication.Views.Chat
{
    public class SenderIdToColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int senderId)
            {
                return senderId == 1 ? 1 : 0; // 2 para coluna da direita (enviada), 0 para coluna da esquerda (recebida)
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
