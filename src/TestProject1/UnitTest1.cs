using FluentAssertions;

namespace TestProject1
{
    public class UnitTest1
    {

        [Theory]
        [InlineData(1, "um reais")]// TODO: Verificar plural e singular.
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
            var chequePorExtenso = new ChequePorExtenso();

            var porExtenso = chequePorExtenso.Escrever(valor);

            porExtenso.Should().Be(valorEsperado);
        }

        [Theory]
        [InlineData(21, "vinte e um reais")]
        public void QuandoPedirParaEscreverChequeComNumerosDezenasUnidades_DeveRetornaroValorPorExtenso(decimal valor, string valorEsperado)
        {
            var chequePorExtenso = new ChequePorExtenso();

            var porExtenso = chequePorExtenso.Escrever(valor);

            porExtenso.Should().Be(valorEsperado);
        }
    }

    public class ChequePorExtenso
    {
        public string Escrever(decimal valor)
        {
            var valorInteiro = Convert.ToInt32(valor);
            var valorDecimal = Convert.ToInt32((valor - valorInteiro) * 100);

            var dezena = valorInteiro / 10;
            var unidade = valorInteiro % 10;

            var retorno = "";
            if (dezena == 1)
                retorno += GetUnidade(valorInteiro % 100);
            else
            {
                retorno += GetDezena(dezena);
                if (unidade > 0)
                {
                    if (dezena > 1)
                        retorno += " e ";
                    retorno += GetUnidade(unidade);
                }
            }

            return retorno + " reais";
        }

        private static string GetUnidade(int valor)
        {
            return valor switch
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
        }

        private static string GetDezena(int valor)
        {
            return valor switch
            {
                2 => "vinte",
                3 => "trinta",
                4 => "quarenta",
                5 => "cinquenta",
                6 => "sessenta",
                7 => "setenta",
                8 => "oitenta",
                9 => "noventa",
                10 => "cem",
                _ => "",
            };
        }
    }
}