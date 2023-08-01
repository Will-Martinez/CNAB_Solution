# Desafio.net_Wilman_Martinez

## Descrição

Este é um projeto de aplicação web que visa atender à demanda de realizar o upload de arquivos no formato cnab, realizar o parse e tratamento dos dados contidos nesses arquivos, e salvá-los em uma base de dados.

## Tecnologias utilizadas

- Microsoft Visual Studio 2022 IDE
- Backend desenvolvido em ASP.NET Core (C#), e o frontend foi desenvolvido usando HTML, CSS e JavaScript, com o framework CSS Bulma e jQuery para a utilização do plugin DataTable.
- Foi utilizada a biblioteca gratuita MongoDB.Driver para C# para conectar a aplicação a uma instância local do banco de dados MongoDB.
- Postman foi utilizado para testar a API.

## Requisitos para o funcionamento do projeto

- Para que o projeto funcione localmente, é necessário ter uma instância local do MongoDB rodando. Caso não tenha instalado, você pode acessar o link: [MongoDB Community](https://www.mongodb.com/try/download/community).
- (Opcional) Existem duas opções para conectar-se a uma base de dados local usando o MongoDB: uma é o shell do MongoDB e a outra é uma ferramenta com interface gráfica chamada Compass, que pode ser encontrada nos seguintes links:
    - [MongoShell](https://www.mongodb.com/try/download/community)
    - [MongoCompass](https://www.mongodb.com/pt-br/products/compass)
- É necessário ter o .NET e seu SDK instalados, na versão 6.0 ou mais recente.

## Como iniciar a aplicação

- Faça o clone do repositório para uma pasta local.
- Com o .NET instalado, abra o terminal do Windows ou Linux e acesse a pasta onde o repositório foi clonado.
- Execute o comando "dotnet build" seguido por "dotnet run". Em seguida, o terminal imprimirá mensagens como mostrado abaixo:

![image](https://github.com/Will-Martinez/Desafio.net_Wilman_Martinez/assets/110312747/a4c81178-b8cc-4cfa-9388-d94e480ab319)


## Funcionamento da aplicação

- Criado um menu dropdown do lado esquerdo da tela para navegar entre as views de home, transactions e upload file
- A aplicação possui duas rotas principais:
    - Rota "upload": Nesta rota, o usuário pode selecionar um arquivo cnab para ser tratado e salvo na base de dados.
    - Rota "transactions": Esta view exibe uma tabela dinâmica contendo todas as transações armazenadas no banco de dados, seja através do formulário de cadastro ou do arquivo cnab.
- Além disso, há uma terceira rota chamada "home", mas ela é usada apenas para apresentação do autor.
- 
![image](https://github.com/Will-Martinez/Desafio.net_Wilman_Martinez/assets/110312747/7f25acdc-c8da-4c44-b1f9-d6fe7afabfbb)

![image](https://github.com/Will-Martinez/Desafio.net_Wilman_Martinez/assets/110312747/f4184257-9325-4884-b3d0-7468d94c12f7)

### Rota "Upload"

Nesta rota, o usuário pode enviar arquivos no formato cnab para salvar suas transações no banco de dados.

### Rota "Transactions"

Nesta rota, estão disponíveis as seguintes funcionalidades:

- **Botão de Detalhes**: Ao clicar neste botão, um modal é aberto, exibindo todos os detalhes da transação realizada.

- **Botão Cadastrar Nova Transação**: Permite o cadastro unitário de uma nova transação sem a necessidade de fazer o upload de um arquivo.

- **Funcionalidades de Filtro na Tabela**: A tabela de transações oferece funcionalidades de filtro, incluindo:
    - Filtro Crescente e Decrescente: Permite ordenar as transações em ordem crescente ou decrescente.
    - Filtro por Nome: Permite filtrar as transações pelo nome.
    - Paginação: A tabela é paginada, facilitando a navegação entre as transações.
    - Quantidade de Elementos por Página: O usuário pode definir a quantidade de elementos a serem exibidos por página.

![image](https://github.com/Will-Martinez/Desafio.net_Wilman_Martinez/assets/110312747/b647b518-2ec8-454d-9aed-d8e006c9ce3c)

![image](https://github.com/Will-Martinez/Desafio.net_Wilman_Martinez/assets/110312747/eb969553-a4df-4dc4-95cf-4b2a253f67a3)



Dessa forma, o projeto atende às necessidades de upload e tratamento de arquivos cnab, além de proporcionar uma visualização clara das transações armazenadas na base de dados MongoDB.
