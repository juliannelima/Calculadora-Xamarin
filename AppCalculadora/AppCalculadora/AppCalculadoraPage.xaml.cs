using Xamarin.Forms;
using System;

namespace AppCalculadora
{
    public partial class AppCalculadoraPage : ContentPage
    {
        #region Declara as variáveis de verificação
        Boolean capturaNovoNumero = true;
        Boolean capturaOperador = false;
        #endregion

        #region Declara as variáveis que receberá os números
        double primeiroNumero = 0;
        double ultimoNumero = 0;
        double resultado = 0;
        #endregion

        //Declara a variável que receberá o operador
        string operador = "";

        public AppCalculadoraPage()
        {
            InitializeComponent();

            #region Inicializa as variáveis de verificação
            capturaNovoNumero = true;
            capturaOperador = false;
            #endregion

            #region Inicializa as variáveis que receberá os números
            primeiroNumero = 0;
            ultimoNumero = 0;
            resultado = 0;
            #endregion

            //Inicializa operador vazio
            operador = "";
        }

        #region Método para os botões numéricos
        void ButtonNumberClicked(object sender, EventArgs e){

            var button = (sender as Button);
            var classId = button.ClassId;

            //Declara variável número
            string numero = "";

            #region Verifica qual classId foi clicado e insere o número na variável numero.
            switch (classId)
            {
                case "btn0": numero = "0"; break;
                case "btn1": numero = "1"; break;
                case "btn2": numero = "2"; break;
                case "btn3": numero = "3"; break;
                case "btn4": numero = "4"; break;
                case "btn5": numero = "5"; break;
                case "btn6": numero = "6"; break;
                case "btn7": numero = "7"; break;
                case "btn8": numero = "8"; break;
                case "btn9": numero = "9"; break;
            }
            #endregion

            //Verifica se é para capturar um novo número
            if (capturaNovoNumero)
                //Insere o primeiro número no visor
                lbVisor.Text = numero;
            else
                //Concatena os números clicados e exibe no visor
                lbVisor.Text = lbVisor.Text + numero;

            //Desabilita captura novo número
            capturaNovoNumero = false;  
            //Habilita captura operador
            capturaOperador = true;
        }
        #endregion

        /// <summary>
        /// Método invocando quando os Botões operadores são clicados
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ButtonOperadorClicked(object sender, EventArgs e)
        {
            #region Verifica se o operador pode ser capturado
            if (capturaOperador)
            {
                var button = (sender as Button);
                var classId = button.ClassId;

                #region Verifica qual classId foi clicado e salva na variavel operador
                switch (classId)
                {
                    case "btnSoma": operador = "+"; break;
                    case "btnDivisao": operador = "/"; break;
                    case "btnSubtracao": operador = "-"; break;
                    case "btnPorcentagem": operador = "%"; break;
                    case "btnMultiplicacao": operador = "x"; break;
                }
                #endregion

                //Salva o primeiro número digitado na variável primeroNumero
                primeiroNumero = Convert.ToDouble(lbVisor.Text);
                //Habilita capturar novo numero
                capturaNovoNumero = true;
            }
            #endregion
        }

        /// <summary>
        /// Método invocado quando o botão mais ou menos for clicado para transformar 
        /// o número em positivo ou negativo
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ButtonMaisOuMenosClicked(object sender, EventArgs e)
        {
            //Verifica se o visor está zerado se sim não faz nada
            if (lbVisor.Text == "0")
                return;

            //Verifica se existe o operador menos antes do número
            if (lbVisor.Text.StartsWith("-"))
                //Retira o operador menos do número
                lbVisor.Text = lbVisor.Text.Replace("-", "");
            else
                //Insere o operador menos no número
                lbVisor.Text = "-" + lbVisor.Text;
        }

        /// <summary>
        /// Método invocado quando o botão ponto for clicado
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ButtonPontoClicked(object sender, EventArgs e)
        {
            //Verifica que está capturando um novo número
            if (capturaNovoNumero)
                //Insere zero e virgula no visor
                lbVisor.Text = "0,";
            //Verifica se não existe virgula no número do visor
            else if (!lbVisor.Text.Contains(","))
                //Concatena a virgual junto com o número do visor
                lbVisor.Text = lbVisor.Text + ",";

            capturaNovoNumero = false;
        }

        /// <summary>
        /// Método invocado quando o botão limpar e clicado limpa todas a variáveis e o visor
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ButtonClearClicked(object sender, EventArgs e)
        {
            #region Inicializa as variáveis de verificação
            capturaNovoNumero = true;
            capturaOperador = false;
            #endregion

            #region Inicializa as variáveis que receberá os números
            primeiroNumero = 0;
            ultimoNumero = 0;
            resultado = 0;
            #endregion

            //Zera o visor
            lbVisor.Text = "0";
            //Limpa variável que receberá o operador
            operador = "";
        }

        /// <summary>
        /// Método invocado quando o botão igual é clicado
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void ButtonIgualClicked(object sender, EventArgs e){
            #region Verifica qual é o operador e chama o método para executar a operação
            switch (operador)
            {
                case "+": ExecutaSoma(); break;
                case "-": ExecutaSubtracao(); break;
                case "/": ExecutaDivisao(); break;
                case "x": ExecutaMultiplicacao(); break;
                case "%": ExecutaPorcentagem(); break;
                default: 
                    //Não existe operador o resultado recebe o número do visor
                    resultado = Convert.ToDouble(lbVisor.Text);
                    lbVisor.Text = resultado.ToString();
                    break;
            }
            #endregion

            //Habilita capturar operador
            capturaOperador = true;
            //Habilita capturar novo número
            capturaNovoNumero = true;
        }

        /// <summary>
        /// Método executa a soma dos números
        /// </summary>
        void ExecutaSoma(){
            //Verifica se a variável primeiro número é diferente de zero e se existe informação no visor
            if (!primeiroNumero.Equals(0) && lbVisor.Text != "")
            {
                //Soma o primeiro número com o número do visor
                resultado = primeiroNumero + Convert.ToDouble(lbVisor.Text);
                //Salva o número do visor na variável último número
                ultimoNumero = Convert.ToDouble(lbVisor.Text);
                //Zera variável primeiro número
                primeiroNumero = 0;
            }
            //Verifica se a variável primeiro número está zerada, se o visor e diferente de vazio e o último número diferente de zero
            else if (primeiroNumero.Equals(0) && lbVisor.Text != "" && !ultimoNumero.Equals(0))
            {
                //soma o último número com o número do visor
                resultado = ultimoNumero + Convert.ToDouble((lbVisor.Text));
            }
            //Insere o resultado no visor
            lbVisor.Text = resultado.ToString();
        }

        /// <summary>
        /// Método executa a subtracão dos números
        /// </summary>
        void ExecutaSubtracao()
        {
            //Verifica se a variável primeiro número é diferente de zero e se existe informação no visor
            if (!primeiroNumero.Equals(0) && lbVisor.Text != "")
            {
                //Subtrai o primeiro número com o número do visor
                resultado = primeiroNumero - (Convert.ToDouble(lbVisor.Text));
                //Salva o número do visor na variável último número
                ultimoNumero = Convert.ToDouble(lbVisor.Text);
                //Zera variável primeiro número
                primeiroNumero = 0;
            }
            //Verifica se a variável primeiro número está zerada, se o visor e diferente de vazio e o último número diferente de zero
            else if (primeiroNumero.Equals(0) && lbVisor.Text != "" && !ultimoNumero.Equals(0))
            {
                //Subtrai o último número com o número do visor
                resultado = ultimoNumero - Convert.ToDouble((lbVisor.Text));
            }
            //Insere o resultado no visor
            lbVisor.Text = resultado.ToString();
        }

        void ExecutaMultiplicacao()
        {
            //Verifica se a variável primeiro número é diferente de zero e se existe informação no visor
            if (!primeiroNumero.Equals(0) && lbVisor.Text != "")
            {
                //Multiplica o primeiro número com o número do visor
                resultado = primeiroNumero * Convert.ToDouble(lbVisor.Text);
                //Salva o número do visor na variável último número
                ultimoNumero = Convert.ToDouble(lbVisor.Text);
                //Zera variável primeiro número
                primeiroNumero = 0;
            }
            //Verifica se a variável primeiro número está zerada, se o visor e diferente de vazio e o último número diferente de zero
            else if (primeiroNumero.Equals(0) && lbVisor.Text != "" && !ultimoNumero.Equals(0))
            {
                //Multiplica o último número com o número do visor
                resultado = ultimoNumero * Convert.ToDouble((lbVisor.Text));
            }
            //Insere o resultado no visor
            lbVisor.Text = resultado.ToString();

        }

        void ExecutaDivisao()
        {
           
        }

        void ExecutaPorcentagem()
        {

        }
    }
}
