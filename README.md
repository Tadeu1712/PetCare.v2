
# **DIS Trabalho Prático: Aplicação Web do PetCare**
---------------------------
### João Santos[2095415], Leonardo Abreu[2067513], Ricardo Jardim[2040416], and Rúben Freitas[2041716]
##                                                             Universidade da Madeira, Madeira, PT
------------------------------------------
**Abstract**. Este relatório procura documentar um projecto de desenvolvimento de uma aplicação web, desde a fase de escolha do tema até as decisões de implementação tomadas para o backend e frontend. Vamos inicialmente identificar o problema que pretendíamos resolver com a nossa aplicação. De seguida iremos explicar as decisões de implementação desde a base de dados,o UML da aplicação, a escolha e implementação dos desenhos padrões  até à interface da aplicação. Porúltimo iremos exemplificar a utilização e discutir a qualidade do produto final, nomeadamente em termos de vantagens/desvantagens para o utilizador.

------------------------------
```
Keywords: C# · Vue · Mysql
```

---------------------------------------

## **1-Introdução**
---------------------------
Este projecto foi realizado no âmbito da cadeira de ”Desenho e Implementação de Software” e tem como objectivo introduzir aos alunos os conceitos fundamentais de desenho e arquitectura de software, os princípios fundamentais do desenho e como utilizar os padrões de desenho de software com a intenção de os aplicar de uma maneira pratica numa linguagem de programação orientada a objectos.

O tema escolhido não estava predefinido, mas sim proposto pelo grupo ao docente, tendo sido apresentado e consequentemente aprovado pelo mesmo. Os pontos referentes à descrição do mesmo e à explicação do problema que com ele pretendíamos resolver serão abordados na próxima secção deste relatório.

## **2-Problema**
----------------------------
O problema que tentámos abordar com este projecto é o aumento constante das taxas de abandono animal e consequentemente a sobrelotação das associações de acolhimento dos mesmos. De acordo com a Associação Nacional de Médicos Veterinários dos Municípios, os números de animais que são recolhidos das ruas em Portugal, pelos centros de recolha oficiais, chegam a atingir valores de cerca de 50 mil por ano, sendo o número de adopções apenas de 17 mil animais anualmente.[3] 

O tema escolhido por nós, face a este problema foi um Website que permite conectar todas as associações de protecção animal da região autónoma da Madeira numa só plataforma. Com isto, o objectivo seria criar uma ligação mais eficiente e intuitiva entre essas associações e os utentes, focando-se, essencialmente, em centralizar toda a informação das mesmas num só ponto.

Uma plataforma que permite aos seus utentes obter informações de várias associações num só lugar. O que irá beneficiar as associações no sentido em que será mais fácil de divulgar campanhas de adopção,vacinação e de esterilização.

## **3-Solução**
-----------------------------------
Para a realização deste projecto, decidimos utilizar o esquema abaixo representado, para a construção da base de dados utilizando a abordagem code-first.
Cada modelo contem o seu respectivo controller que irá realizar a interligação entre os dados e armazena-los na base de dados.

```
Fig. 1.Diagrama da Base de Dados
```

### **3.1-Tecnologias**
---------------------------------
Nesta secção iremos abordar as tecnologias usadas para a realização deste trabalho, explicando o motivo por as termos escolhido e como é que as utilizamos.

#### **3.1.1-Entity Framework Core**

O Enity Framework Core 2.0 suporta duas abordagens de desenvolvimento:

1. Code-First.
2. Database-First.

Na abordagem code-first, o EF Core API cria a base de dados e tabelas utilizando a migração, com base nas convenções e configurações fornecidas nas suas classes. Na Database-First, o EF Core API cria as classes e contexto com base na sua base de dados existente, usando os comandos do EF Core.[1]

A abordagem escolhida para este projecto foi a code-first uma vez que a EF oferece um melhor suporte à esta abordagem. Para além disso o Entity Framework core 2.0 não oferece qualquer tipo de suporte para o modelo da Base de dados.[1]

#### **3.1.2-Vue.js Framework**
Vue é uma estrutura progressiva para a construção de user interfaces. Ao contrário de outras frameworks, Vue é desenhado desde o início para ser incrementalmente adaptável. A biblioteca principal foca-se apenas na camada da view, o que torna simples integrar outras bibliotecas ou projectos existentes. Por outro lado, o Vue também é perfeitamente capaz de alimentar Single-Page Applications, quando usado em combinação com ferramentas modernas e bibliotecas de suporte.

### **3.2-Padrões de desenho**
---------------------------------------------
#### **3.2.1-Factory Method**
É um padrão de desenho do tipo criação que fornece uma interface para criar objectos em uma superclasse, mas que permite que as subclasses alterem o tipo de objectos que serão criados.
````
Ricardo explica melhor
````
#### **3.2.2-State**
É um padrão de desenho do tipo comportamental que permite que um objecto altere o seu comportamento quando o seu estado interno muda. Parece que o objecto modificou a sua classe. Utilizamos este padrão para facilitar a gestão do estado em que o animal se encontra.
Qualquer animal pertencente a uma associação pode ter um destes 3 estados :

- Adotado.
- Para adoção.
- Perdido.

#### **3.2.3-Singleton**
É um padrão de desenho do tipo criação que garante que uma classe tenha apenas uma instância, ao mesmo tempo que lhe proporciona um ponto de acesso global a essa instância.
Este padrão de desenho é utilizado em todas as factories, uma vez que só queremos instanciar uma única vez esse objecto.

#### **3.2.4-Observer**
É um padrão de desenho do tipo comportamental que permite definir um mecanismo de subscrição para notificar múltiplos objectos sobre qualquer
evento/mudança que aconteça ao objecto que eles estão observando.

#### **3.2.5-Single File Components**
O padrão Single File Components (SFC)é um padrão desenho utilizado no lado do cliente, ou seja este é utilizado na framework javascript Vuejs de modo a organizar e optimizar o desenvolvimento do mesmo.
O padrão referido anteriormente permite o isolamento de componentes, garantido uma independência relativa entre HTML, CSS e JavaScript, desta forma é possível eliminar a instanciação de valores globais, reduzir complexidade do código através de simplificação de HTML, suporte integrado de CSS, maximizar a liberdade de tecnologias de desenvolvimento, módulos CommonJS e limitações de CSS para componentes individuais.
Este é o padrão de desenvolvimento mais utilizado no lado do cliente, pois pode ser implementado de forma consistente em cada componente, onde as secções de template, lógica e estilo são fortemente acoplados e a sua implementação torna o componente coesivo facilitando a sua manutenção [2].

#### **3.2.6-Functional components**
 De forma de semelhante ao padrão anterior, este trata-se também de um padrão utilizado no lado do cliente que se refere a uma especialização dos SFC onde exclui a necessidade de um estado. Os benefícios desta tipologia de componentes são relativos a performance, nomeadamente nos tempos de renderização das páginas e otimização na utilização de memória. 

Estes componentes descritos anteriormente possuem a particularidade de não
necessitarem da secção responsável pela lógica, pois todos os dados necessários são transmitidos através de props e eventos proveniente de componentes de níveis distintos [2].

#### **3.2.6-Component Communication**
Finalmente para finalizar a secção relativa a padrões no no lado do cliente, a Componente Communication permite uma comunicação eficiente entre componentes de níveis distintos, facilitando assim a reutilização de informação sem utilização de recursos desnecessários.

Os componentes VueJs seguem um protocolo de comunicação unilateral com componentes vizinhas, que consiste na utilização de eventos no sentido superior e props no sentido inferior. Estes últimos são dados reactivos de leitura, ou seja componentes filhos não conseguem alterar os seus valores, mas quando estes alteram, os componentes filhos que os utilizam são novamente renderizados. Os componentes filhos poderão enviar pedidos de alteração de valores dos props através da emissão de eventos para o componente pai, de modo a que este altere os dados em questão mapeados no componente filho [2].

## **4-Utilização**
--------------------------------------
A plataforma web desenvolvida está diferenciada através de duas secções distintas, nomeadamente as páginas acessíveis para todos os visitantes
onde é possível encontrar todas as funcionalidades básicas e um painel administrativos para associações e administrador.

### **4.1- Painel administrativo**
---------------------------------------
Através do painel administrativo o administrador poderá executar as acções CRUD (criação, consulta, actualização e destruição de dados) sobre os utilizadores registados na plataforma, comparativamente as associações poderão executar estas mesmas acções CRUD sobre os animais, eventos e novidades pertencentes a esta associação em questão. Adicionalmente é ainda possível à associação actualizar o seu próprio perfil.
```
PRINTS
```
### **4.2- Animais**
----------------------------------------
Os animais inseridos na plataforma poderão ser acedidos em diversas páginas da interface. Ao explorar o website poderemos encontrar na página inicial uma listagem ”infinita” de todos os animais, de todas as associações ordenados por ordem de colocação na plataforma do mais recente. Esta abordagem foi escolhida de modo a captar de formai mais rápida possível a atenção do visitante.

Cada animal é apresentado em formato carta fornecendo assim individualização e independência que poderão cá possíveis utilizadores. Adicionalmente poderemos ainda carregar em qualquer animal para visualizar dados mais detalhados como por exemplo peso, porte, idade, entre outros..

```
PRINTS
````
Poderão ainda ser visualizados animais pertencentes de uma dada associação através do perfil público dessa mesma associação, na página ”Amiguinhos” que será descrita numa sub-secção seguinte e numa tabela pertencente ao painel administrativo da associação em questão.

```
PRINTS
````

### **4.3- Eventos**
------------------------------
De forma semelhante à página principal descrita anteriormente, os eventos possuem também uma secção dedicada à sua apresentação.E também utilizada ́ uma listagem ”infinita” ordenada por eventos com data de inicialização mais próxima, não sendo apresentados eventos que já tinham acontecido. Os eventos estão também presentes no perfil público duma dada associação e no seu painel administrativo.

```
PRINTS
````

### **4.4- Perdidos**
------------------------------
A página para animais perdidos é um pouco diferente das descritas anteriormente, pois esta tem como objectivo a inserção de dados por qualquer visitante da plataforma, não estando associada com qualquer tipo de utilizador ou associação.

A página é constituída por um formulário que qualquer utilizador poderá preencher com o objectivo de dar como perdido um dado animal, onde são
fornecidos dados de contacto e descrições do animal em questão. Desta forma outros utilizadores que visitem a página poderão ajudar no que pode ser melhor caracterizado como ”perdidos & achados” baseado em animais domésticos.

```
PRINTS
````

### **4.5- Amiguinhos**
--------------------------------------------
Para finalizar esta última secção visa dinamizar e cativar um grande vertente dos utilizadores, jovens. Aqui são apresentados de forma muito mais infantil e dinâmica os animais bebés (e.x ¡1 ano) com auxílio de animações e efeitos visuais.

## **5-Discussão**
------------------------------
Nesta secção iremos abordar as vantagens e as desvantagens da nossa aplicação web do ponto de vista da utilização do utente.


### **5.1- Vantagens**
---------------------------------
- Reúne todas as informações de todas as principais associações de protecção
    animal num sitio só. O que permite aos utilizadores uma maior facilidade ao
    navegar pelas associações.
- Acesso fácil a um catalogo de animais para adopção.
- Facilidade que publicar um post para um animal perdido.

### **5.2- Desvantagens**
-----------------------------------
- Não possui de opção directa de doação a uma associação, mas fornecemos
    dados para tal.
-

## **6-Conclusão**
------------------------------------
Como já tínhamos referido anteriormente, achamos que os principais critérios a ter em conta ao avaliar o produto final são as vantagens/desvantagens do ponto de vista do utilizador, e se a aplicação realmente resolve o problema que se propôs a resolver. Nomeadamente, voltar a comparar a solução com outros sites/ferramentas que ofereçam ao utente a mesma função. Nesse aspecto, penso que tentamos responder a todas as questões que levantamos no problema.
O único problema que antecipamos é a possibilidade de existir utentes não familiarizados com aplicações informáticas que possam ter dificuldades em utilizar o sistema. Esse problema não pode ser efectivamente resolvido, mas tentamos sempre minimiza-lo procurando simplificar ao máximo a interface da aplicação.



##  **References**
---------------------------
1. EF. 2020. Entity Framework Core. (2020).
    https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx.
2. Vue Patterns. 2020. Vue Patterns. (2020). https://learn-vuejs.github.io/vue-
    patterns/patterns/component-declaration.
3. TVI24. 2019. Cerca de 50 mil animais abandonados são resgatados das ruas por-
    tuguesas por ano. (2019). https://tvi24.iol.pt/sociedade/abandono/cerca-de-50-
    mil-animais-abandonados-sao-recolhidos-das-ruas-portuguesas-por-ano.

## 7 Anexos

