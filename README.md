# EW Task Management Application

Bem-vindo ao **EW Task Management Application**! Este aplicativo é uma solução para gerenciar tarefas, projetos e interações entre usuários de forma eficiente.

---

## Executando o Projeto com Docker

### Pré-requisitos

- **Docker** instalado em sua máquina.
- **SQL Server** acessível (localmente ou em um contêiner Docker).

### Passos para Executar

1. **Clone o Repositório**

   ```bash
   git clone https://github.com/seu-usuario/ew-task-management.git
   cd ew-task-management
   ```

2. **Configurar a String de Conexão**

   Atualize a string de conexão no arquivo `appsettings.json` do projeto `EW.TaskManagement.API` para apontar para o seu servidor SQL Server.

   **Exemplo de `appsettings.json`:**

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=host.docker.internal,1433;Database=ew-task-management;User Id=ew_tm_api;Password=SuaSenhaAqui;TrustServerCertificate=True;"
     }
   }
   ```

   **Nota:** O endereço `host.docker.internal` permite que o contêiner Docker acesse serviços rodando na máquina host.

3. **Executar o Projeto com Docker**

   Construa a imagem Docker e execute o contêiner:

   ```bash
   docker build -t ew-task-management .
   docker run -d -p 8080:80 --name ew-task-management-app ew-task-management
   ```

   Isso irá:

   - Construir a imagem Docker usando o `Dockerfile`.
   - Executar o contêiner e mapear a porta `80` do contêiner para a porta `8080` da sua máquina.

4. **Acessar a Aplicação**

   Abra o navegador e acesse `http://localhost:8080`.

---

## Configurações Adicionais

### Configurar o Banco de Dados

1. **Criar a Base de Dados e Usuário**

   No SQL Server, crie a base de dados `ew-task-management` e um usuário com as permissões necessárias.

   **Exemplo de Scripts SQL:**

   ```sql
   CREATE DATABASE [ew-task-management];
   GO

   USE [ew-task-management];
   GO

   CREATE LOGIN [ew_tm_api] WITH PASSWORD = 'SuaSenhaAqui';
   GO

   CREATE USER [ew_tm_api] FOR LOGIN [ew_tm_api];
   GO

   ALTER ROLE [db_datareader] ADD MEMBER [ew_tm_api];
   ALTER ROLE [db_datawriter] ADD MEMBER [ew_tm_api];
   GO
   ```

2. **Executar as Migrações**

   Execute as migrações para criar as tabelas no banco de dados:

   ```bash
   dotnet ef database update
   ```

3. **Inserir um Usuário**

   Como não há um controlador para criar usuários via API, insira um usuário diretamente no banco de dados:

   ```sql
   INSERT INTO [dbo].[Users] ([Name], [Role])
   VALUES ('Admin', 1);
   GO
   ```

---

## Observações

- **Porta do SQL Server:** Certifique-se de que o SQL Server está configurado para aceitar conexões na porta `1433`.

- **Autenticação do SQL Server:** Habilite a autenticação SQL Server e Windows (modo misto).

- **Firewall e Rede:** Se estiver usando o SQL Server localmente, configure o firewall para permitir conexões na porta `1433` e habilite o protocolo TCP/IP no SQL Server Configuration Manager.

---

## Comandos Úteis

- **Parar o Contêiner Docker:**

  ```bash
  docker stop ew-task-management-app
  ```

- **Remover o Contêiner Docker:**

  ```bash
  docker rm ew-task-management-app
  ```

- **Remover a Imagem Docker:**

  ```bash
  docker rmi ew-task-management
  ```

---
## Questões para o Product Owner

1. **Autenticação e Autorização de Usuários:**
   - Há planos para implementar autenticação de usuários (por exemplo, login, logout) na aplicação?
   - Devemos considerar a integração com provedores de autenticação externos (como OAuth, OpenID) ou utilizar um sistema personalizado?
   - Quais níveis de autorização são necessários? Por exemplo, papéis como Administrador, Gerente, Usuário Padrão com diferentes permissões.

2. **Gerenciamento de Usuários:**
   - Os usuários poderão se registrar por conta própria, ou as contas serão criadas por um administrador?
   - Precisamos implementar funcionalidades de gerenciamento de senhas (por exemplo, recuperação de senha, políticas de senha)?
   - Será necessário um perfil de usuário onde eles possam atualizar suas informações?

3. **Atribuição e Colaboração em Tarefas:**
   - As tarefas podem ser atribuídas a usuários específicos ou a múltiplos usuários?
   - Os usuários poderão delegar ou reatribuir tarefas a outros?
   - Devemos implementar notificações (por exemplo, e-mail, notificações in-app) quando tarefas são atribuídas ou atualizadas?

4. **Relatórios e Análises Adicionais:**
   - Existem outros tipos de relatórios ou métricas que seriam valiosos para os usuários?
   - Os relatórios devem ser personalizáveis ou exportáveis (por exemplo, para PDF, Excel)?

5. **Integração com Outros Sistemas:**
   - Há necessidade de integrar a aplicação com outras ferramentas (como calendários, sistemas de acompanhamento de tempo, ferramentas de gerenciamento de código)?
   - A aplicação deve expor APIs para integração com outros serviços?

6. **Internacionalização:**
   - A aplicação precisará suportar múltiplos idiomas?

7. **Registro de Auditoria e Logs:**
    - A aplicação deve manter registros detalhados das ações dos usuários para fins de auditoria (além do registro de histórico da task)?
    - Qual nível de detalhe é necessário para esses logs?

8. **Desempenho e Escalabilidade:**
    - Quais são as expectativas em termos de número de usuários simultâneos e volume de dados?

9. **Gestão de Versões:**
    - Como as atualizações e novas funcionalidades serão implantadas?

10. **Cloud:**
    - Existem restrições ou preferências quanto ao uso provedores (GCP, AWS, AZURE)?

---

## Melhorias e Próximos Passos
### 1. **Implementação de Autenticação e Autorização**

- **Identity Framework:**
- **JWT Tokens:**

### 2. **Implementação de Padrões de Projeto**

- **Repository e Unit of Work:**
  - Refinar a implementação dos padrões **Repository** e **Unit of Work** para melhor separação de preocupações.
- **CQRS (Command Query Responsibility Segregation):**
  - Implementar o padrão **CQRS** para separar operações de leitura e escrita.

### 3. **Integração Contínua e Entrega Contínua (CI/CD)**

- Configurar pipelines de **CI/CD** usando ferramentas como **GitHub Actions**, **Azure DevOps** ou **Jenkins**.

### 4. **Monitoramento e Logging**

- Implementar um sistema de logging robusto usando o **Serilog** ou **NLog**, e integrar com ferramentas de monitoramento como **Application Insights**.

### 5. **Considerações de Arquitetura em Nuvem**

- **Containerização Completa:**
  - Containerizar não apenas a aplicação, mas também o banco de dados usando o **Docker Compose**.
- **Orquestração com Kubernetes:**
  - Preparar a aplicação para implantação em ambientes orquestrados como o **Kubernetes**.
- **Serviços em Nuvem:**
  - Considerar o uso de serviços gerenciados como o **Azure SQL Database**, **AWS RDS** ou **Google Cloud SQL**.

### 6. **Internacionalização (Depende da respostas do PO)**

- Implementar suporte a múltiplos idiomas usando recursos de **localização** do .NET.

### 7. **Validação e Segurança de Dados**

- Implementar validações mais robustas usando **Data Annotations** e **Fluent Validation**.
- Realizar análises de segurança para identificar e mitigar vulnerabilidades (por exemplo, injeção de SQL, XSS).

### 8. **Performance e Otimizações**

- Implementar cache para dados frequentemente acessados usando **Redis** ou cache em memória.
- Revisar as consultas do Entity Framework para otimizar o carregamento de dados (Lazy Loading vs. Eager Loading).

### 9. **Interface do Usuário e Front-End**

- Desenvolver uma interface web ou mobile usando frameworks modernos como **Angular**, **React** ou **Blazor**.

### 10. **Métricas e Telemetria**

- Coletar métricas de uso e desempenho para análise e melhoria contínua (complexidade ciclomática, assim como usamos records com init).

### 11. **Testes Automatizados e Qualidade**

- **Testes de Integração e Aceitação:**
  - Expandir a suíte de testes para incluir testes de integração e aceitação usando ferramentas como **Selenium** ou **SpecFlow**.
- **Análise de Código:**
  - Integrar ferramentas de análise como **SonarQube** para identificar problemas de código (code smells).

### 12. **Documentação**

- Manter documentação atualizada, incluindo diagramas de arquitetura (pelo próprio VS), manuais de usuário (e BRD (business requirements document) e combinar padronização com o time.
