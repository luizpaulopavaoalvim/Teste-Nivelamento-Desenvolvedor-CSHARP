using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria {
        // Propriedades com acesso privado para encapsulamento
        public int Numero { get; }
        public string Titular { get; private set; }
        private double Saldo { get; set; }

        // Construtor com depósito inicial opcional
        public ContaBancaria(int numero, string titular, double depositoInicial = 0.0) {
            Numero = numero;
            Titular = titular;
            Saldo = depositoInicial;
        }

        // Método para realizar depósito
        public void Deposito(double quantia) {
            Saldo += quantia;
        }

        // Método para realizar saque
        public void Saque(double quantia) {
            // Cobrar taxa de $3.50 por saque
            const double taxaSaque = 3.50;

            if (quantia + taxaSaque > Saldo) {
                Console.WriteLine("Saldo insuficiente para realizar o saque.");
            } else {
                Saldo -= quantia + taxaSaque;
                Console.WriteLine($"Saque de {quantia:C} realizado. Taxa de saque: {taxaSaque:C}");
            }
        }

        // Método para alterar o nome do titular
        public void AlterarTitular(string novoTitular) {
            Titular = novoTitular;
        }

        // Sobrescrevendo o método ToString para exibir informações da conta
        public override string ToString() {
            return $"Número da conta: {Numero}, Titular: {Titular}, Saldo: {Saldo:C}";
        }
    }
}
