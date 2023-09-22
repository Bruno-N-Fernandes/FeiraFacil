using FluentAssertions;
using System.Drawing;

namespace TestProject1
{
    public class UnitTest1
    {

        [Theory]
        [InlineData(1, "um real")]
        [InlineData(2, "dois reais"), InlineData(3, "três reais"), InlineData(4, "quatro reais")]
        [InlineData(5, "cinco reais"), InlineData(6, "seis reais"), InlineData(7, "sete reais")]
        [InlineData(8, "oito reais"), InlineData(9, "nove reais"), InlineData(10, "dez reais")]

        [InlineData(11, "onze reais"), InlineData(12, "doze reais"), InlineData(13, "treze reais")]
        [InlineData(14, "quatorze reais"), InlineData(15, "quinze reais"), InlineData(16, "dezesseis reais")]
        [InlineData(17, "dezessete reais"), InlineData(18, "dezoito reais"), InlineData(19, "dezenove reais")]

        [InlineData(20, "vinte reais"), InlineData(30, "trinta reais"), InlineData(40, "quarenta reais")]
        [InlineData(50, "cinquenta reais"), InlineData(60, "sessenta reais"), InlineData(70, "setenta reais")]
        [InlineData(80, "oitenta reais"), InlineData(90, "noventa reais"), InlineData(100, "cem reais")]
        public void QuandoPedirParaEscreverChequeComNumerosInteiros_DeveRetornaroValorPorExtenso(decimal valor, string valorEsperado)
        {
            var chequePorExtenso = new ChequePorExtenso(valor);

            var porExtenso = chequePorExtenso.Escrever();

            porExtenso.Should().Be(valorEsperado);
        }

        [Theory]
        [InlineData(21, "vinte e um reais")]
        [InlineData(22, "vinte e dois reais")]
        [InlineData(33, "trinta e três reais")]
        [InlineData(234, "duzentos e trinta e quatro reais")]
        [InlineData(1234, "um mil, duzentos e trinta e quatro reais")]
        [InlineData(9234, "nove mil, duzentos e trinta e quatro reais")]
        [InlineData(987234, "novecentos e oitenta e sete mil, duzentos e trinta e quatro reais")]
        [InlineData(1987234, "um milhão, novecentos e oitenta e sete mil, duzentos e trinta e quatro reais")]
        [InlineData(1987654321, "um bilhão, novecentos e oitenta e sete milhões, seiscentos e cinquenta e quatro mil, trezentos e vinte e um reais")]
        [InlineData(2987654321, "dois bilhões, novecentos e oitenta e sete milhões, seiscentos e cinquenta e quatro mil, trezentos e vinte e um reais")]
        [InlineData(537_412_987_654_321, "quinhentos e trinta e sete trilhões, quatrocentos e doze bilhões, novecentos e oitenta e sete milhões, seiscentos e cinquenta e quatro mil, trezentos e vinte e um reais")]
        [InlineData(111_111_111_111_111, "cento e onze trilhões, cento e onze bilhões, cento e onze milhões, cento e onze mil, cento e onze reais")]
        [InlineData(999_999_999_999_999, "novecentos e noventa e nove trilhões, novecentos e noventa e nove bilhões, novecentos e noventa e nove milhões, novecentos e noventa e nove mil, novecentos e noventa e nove reais")]
        public void QuandoPedirParaEscreverChequeComNumerosDezenasUnidades_DeveRetornaroValorPorExtenso(decimal valor, string valorEsperado)
        {
            var chequePorExtenso = new ChequePorExtenso(valor);

            var porExtenso = chequePorExtenso.Escrever();

            porExtenso.Should().Be(valorEsperado);
        }

        [Fact]
        public void QuandoPedirParaEscreverChequeComTudoUm_DeveRetornaroValorPorExtenso()
        {
            var valorEsperado = "cento e onze trilhões, cento e onze bilhões, cento e onze milhões, cento e onze mil, cento e onze reais e onze centavos";

            var chequePorExtenso = new ChequePorExtenso(111_111_111_111_111.11M);

            var porExtenso = chequePorExtenso.Escrever();

            porExtenso.Should().Be(valorEsperado);
        }

        [Fact]
        public void QuandoPedirParaEscreverChequeComTudoNove_DeveRetornaroValorPorExtenso()
        {
            var valorEsperado = "novecentos e noventa e nove trilhões, novecentos e noventa e nove bilhões, novecentos e noventa e nove milhões, novecentos e noventa e nove mil, novecentos e noventa e nove reais e noventa e nove centavos";

            var chequePorExtenso = new ChequePorExtenso(999_999_999_999_999.99M);

            var porExtenso = chequePorExtenso.Escrever();

            porExtenso.Should().Be(valorEsperado);
        }
    }

    public class ChequePorExtenso
    {
        private readonly decimal _valorOriginal;
        private decimal _valorAtual;

        public decimal ValorOriginal => _valorOriginal;
        public long ValorInteiro => Convert.ToInt64(Math.Truncate(_valorAtual));
        public long ValorDecimal => Convert.ToInt64(Math.Truncate((_valorAtual - ValorInteiro) * 100));

        public long Centena => ValorInteiro % 1000;
        public long Dezena => ValorInteiro % 100;
        public long Unidade => ValorInteiro % 10;

        public long DigitoCentena => Centena / 100;
        public long DigitoDezena => Dezena / 10;
        public long DigitoUnidade => Unidade / 1;


        public ChequePorExtenso(decimal valor)
        {
            _valorOriginal = valor;
        }

        public string Escrever()
        {
            var retorno = "";

            _valorAtual = _valorOriginal / 1_000_000_000_000;
            retorno += EscreverCore(" trilhão, ", " trilhões, ");

            _valorAtual = _valorOriginal / 1_000_000_000;
            retorno += EscreverCore(" bilhão, ", " bilhões, ");

            _valorAtual = _valorOriginal / 1_000_000;
            retorno += EscreverCore(" milhão, ", " milhões, ");

            _valorAtual = _valorOriginal / 1_000;
            retorno += EscreverCore(" mil, ", " mil, ");

            _valorAtual = _valorOriginal / 1;
            retorno += EscreverCore("", "");

            retorno += GetMoeda(ValorInteiro);

            _valorAtual = ValorDecimal;
            if (_valorAtual > 0)
                retorno += " e " + EscreverCore(" centavo", " centavos");

            return retorno;
        }

        public string EscreverCore(string sufixoSingular, string sufixoPlural)
        {
            var retorno = "";

            retorno += GetCentena(DigitoCentena, DigitoDezena + DigitoUnidade > 0);

            if (DigitoDezena > 1)
            {
                retorno += GetDezena(DigitoDezena, DigitoUnidade > 0);
                retorno += GetUnidade(DigitoUnidade, sufixoSingular, sufixoPlural);
            }
            else
                retorno += GetUnidade(Dezena, sufixoSingular, sufixoPlural);

            return retorno;
        }

        private string GetMoeda(long valorInteiro)
        {
            return valorInteiro switch
            {
                0 => "",
                1 => " real",
                _ => " reais",
            };
        }

        private static string GetUnidade(long valor, string sufixoSingular, string sufixoPlural)
        {
            var retorno = valor switch
            {
                1 => "um",
                2 => "dois",
                3 => "três",
                4 => "quatro",
                5 => "cinco",
                6 => "seis",
                7 => "sete",
                8 => "oito",
                9 => "nove",
                10 => "dez",
                11 => "onze",
                12 => "doze",
                13 => "treze",
                14 => "quatorze",
                15 => "quinze",
                16 => "dezesseis",
                17 => "dezessete",
                18 => "dezoito",
                19 => "dezenove",
                _ => "",
            };

            if (!string.IsNullOrWhiteSpace(retorno))
                retorno += (valor <= 1) ? sufixoSingular : sufixoPlural;

            return retorno;
        }

        private static string GetDezena(long valor, bool plural)
        {
            var retorno = valor switch
            {
                1 => "dez",
                2 => "vinte",
                3 => "trinta",
                4 => "quarenta",
                5 => "cinquenta",
                6 => "sessenta",
                7 => "setenta",
                8 => "oitenta",
                9 => "noventa",
                _ => "",
            };

            if (!string.IsNullOrWhiteSpace(retorno))
                retorno += (plural ? " e " : "");

            return retorno;
        }

        private static string GetCentena(long valor, bool plural)
        {
            var retorno = valor switch
            {
                1 => plural ? "cento" : "cem",
                2 => "duzentos",
                3 => "trezentos",
                4 => "quatrocentos",
                5 => "quinhentos",
                6 => "seiscentos",
                7 => "setecentos",
                8 => "oitocentos",
                9 => "novecentos",
                _ => "",
            };

            if (!string.IsNullOrWhiteSpace(retorno))
                retorno += (plural ? " e " : "");

            return retorno;
        }
    }
}