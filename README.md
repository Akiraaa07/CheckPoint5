# CheckPoint 5 - API com Microservices e RabbitMQ

## 🎯 Objetivo

Este projeto implementa uma arquitetura baseada em microserviços utilizando RabbitMQ como middleware para mensageria, simulando um fluxo de validação de dados entre diferentes serviços.

---

## 🧩 Estrutura dos Projetos

- **SenderFrutas**: Envia informações de frutas da época (nome e descrição) para validação.
- **SenderUsuarios**: Envia dados de usuários (nome, endereço, RG e CPF) para validação.
- **Validation**: Responsável por validar as mensagens recebidas dos senders e repassar para os receivers.
- **ReceiverFrutas**: Recebe frutas validadas.
- **ReceiverUsuarios**: Recebe usuários validados.

---

## 🛠️ Tecnologias Utilizadas

- .NET Console App (.NET 6)
- RabbitMQ (via instalação local)
- Mensageria com `RabbitMQ.Client`

---

## 🗂️ Filas e Exchanges

| Exchange           | Fila                | Descrição                          |
|--------------------|---------------------|-------------------------------------|
| frutas_exchange    | frutas              | Fila de entrada de frutas           |
| frutas_exchange    | frutas_validadas    | Fila de frutas validadas            |
| usuarios_exchange  | usuarios            | Fila de entrada de usuários         |
| usuarios_exchange  | usuarios_validados  | Fila de usuários validados          |

---

## 🚀 Execução

1. Certifique-se que o RabbitMQ está rodando localmente (`http://localhost:15672`)
2. Rode todos os 5 projetos como **projetos de inicialização múltiplos**
3. Verifique no painel RabbitMQ se as filas estão sendo criadas e processadas corretamente.

---

## 📸 Print do Painel RabbitMQ

![print_filas](https://github.com/user-attachments/assets/f3aa9792-0045-4a0a-8670-0938866ebf34)

---

## 👨‍💻 Integrantes

- **Igor Akira Bortolini Tateishi** — RM554227
