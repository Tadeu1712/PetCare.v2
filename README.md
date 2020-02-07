
# **DIS Trabalho Prático: Aplicação Web do PetCare**

### João Santos[2095415], Leonardo Abreu[2067513], Ricardo Jardim[2040416], and Rúben Freitas[2041716]
##                                                             Universidade da Madeira, Madeira, PT

**Abstract**. Este relatório procura documentar um projeto de desenvolvimento de uma aplicação web desde a fase de escolha do tema até as decisões de implementação tomadas para o backend e frontend. Inicialmente identificou-se o problema que pretendíamos resolver com a nossa aplicação. De seguida iremos explicar as decisões de implementação desde a base de dados, o UML da aplicação, a escolha e implementação dos padrões de desenho até à interface da aplicação. Por último iremos exemplificar a utilização e discutir a qualidade do produto final, nomeadamente em termos de vantagens/desvantagens para o utilizador.


```
Keywords: C# · Vue · Mysql
```

## **1-Introdução**

Este projeto foi realizado no âmbito da cadeira de ”Desenho e Implementação de Software” e tem como objetivo introduzir aos alunos os conceitos fundamentais de desenho e arquitetura de software, os princípios fundamentais do desenho e como utilizar os padrões de desenho de software com a intenção de os aplicar de uma maneira pratica numa linguagem de programação orientada a objetos.

Este relatório terá como objetivo documentar um projeto de desenvolvimento de uma aplicação web, desde de uma fase inicial onde iremos explicar o problema que estamos a tentar abordar e qual a solução que decidimos adotar.

O tema escolhido não estava predefinido, mas sim proposto pelo grupo ao docente, tendo sido apresentado e consequentemente aprovado pelo mesmo. Os pontos referentes à descrição do mesmo e à explicação do problema que com ele pretendíamos resolver serão abordados na próxima secção deste relatório.

## **2-Problema**

O problema que abordamos com este projeto consiste no aumento constante das taxas de abandono animal e consequentemente a sobrelotação das associações de acolhimento dos mesmos. De acordo com a Associação Nacional de Médicos Veterinários dos Municípios, os números de animais que são recolhidos das ruas, em Portugal, pelos centros de recolha oficiais, chegam a atingir valores de cerca de 50 mil por ano, sendo que, o número de adoções são apenas de 17 mil animais anualmente.[3]

Depois de uma breve pesquisa sobre as Associações Protetoras de animais na RAM, observamos que a informação está muito dispersa, ou seja, se quisermos encontrar uma campanha de vacinação, adoção ou de esterilização é necessário navegar em inúmeros sites para saber qual é a associação que estará a realizar tal campanha, sendo que, isto consome muito tempo.

Até mesmo se quisermos encontrar um animal de estimação para adotar com um certo tamanho, idade, cor ou personalidade teríamos que realizar inúmeras pesquisas em diversos websites, onde a maioria desses sites não possuem nenhum tipo de feature que deixe pré-visualizar  os animais que estão disponíveis para adoção, muito menos qualquer tipo de informação acerca dos mesmos. Uma das únicas opções possíveis neste momento, é dirigir-se pessoalmente ao local da instituição.

O tema escolhido por nós, face a este problema foi um Website que permite conectar todas as associações de proteção animal da região autónoma da Madeira numa só plataforma. Com isto, o objetivo seria criar uma ligação mais eficiente e intuitiva entre essas associações e os utilizadores, focando-se, essencialmente, em centralizar toda a informação das mesmas num só ponto.

Uma plataforma que permite aos seus utilizadores obter informações de várias associações e dos animais que cada uma possui para adoção num só lugar. O que irá beneficiar as associações no sentido em que será mais fácil de divulgar campanhas de adoção, vacinação e de esterilização.

## **3-Solução**
A solução que chegamos perante ao problema em questão, foi a criação do website PetCare que permitirá que todas as associações, numa fase inicial da Região Autónoma da Madeira, se registem e que partilhem todas as suas informações, nomeadamente campanhas que pretendem realizar e animais que tenham para adoção, tudo em uma só plataforma.

Logo os utilizadores têm acesso a uma plataforma onde conseguem aceder a informações de todas as associações inclusivo todos os animais que estão disponíveis para adoção via uma feature de pré-visualização, que irá mostrar a foto do animal, descrição e personalidade.


### **3.1-Arquitectura**

![Image of Database](https://raw.githubusercontent.com/Tadeu1712/PetCare.v2/master/Images/PetCareDB.png)

Fig. 1.Diagrama da Base de Dados


```
Fig. 2.UML
```

### **3.2-Tecnologias**
Nesta secção iremos abordar as tecnologias usadas para a realização deste trabalho, explicando o motivo por as termos escolhido e como é que as utilizamos.

#### **3.2.1-Entity Framework Core**

O Enity Framework Core 2.0 suporta duas abordagens de desenvolvimento:

1. Code-First.
2. Database-First.

Na abordagem code-first, o EF Core API cria a base de dados e tabelas utilizando a migração, com base nas convenções e configurações fornecidas nas suas classes. Na Database-First, o EF Core API cria as classes e contexto com base na sua base de dados existente, usando os comandos do EF Core.[1]

A abordagem escolhida para este projeto foi a code-first uma vez que nós foi recomentado pelo docente da unidade curricular e o facto que a EF oferece um melhor suporte à esta abordagem. Para além disso o Entity Framework core 2.0 não oferece qualquer tipo de suporte para o modelo da Base de dados.[1]

#### **3.1.2-Vue.js Framework**
Vue é uma estrutura progressiva para a construção de interfaces. Ao contrário de outras frameworks, Vue é desenhado desde o início para ser incrementalmente adaptável. A biblioteca principal foca-se apenas na camada da view, o que torna simples integrar outras bibliotecas ou projetos existentes. Evan You criou o Vue com a ajuda de centenas de membros da comunidade, e os programadores usaram o framework em quase 1,2 milhões de projetos, de acordo com dados do GitHub. Dito isto, devido ao seu rápido crescimento e resultados positivos, decidimos então, incorporar esta framework neste projeto.

### **3.3-Padrões de desenho**
Padrões de desenho são uma solução para um problema num certo contexto, onde tal contexto é a situação para a qual se aplica um desenho(deve ser uma situação recorrente). O problema são os constrangimentos que ocorrem no contexto. A solução é um desenho geral que podemos aplicar de forma a resolver os constrangimentos.

#### **3.3.1-Factory Method**
Um factory method consiste num padrão de desenho do tipo criação, que permite uma abstração para criar tipos de objetos em que deixe as subclasses decidirem que class será instanciada. A partir deste padrão é possível evitar um acoplamento rígido entre o criador e os produtos em concreto, em que respeita o Princípio da responsabilidade única, Princípio Aberto-Fechado e Princípio da inversão da dependência. Utilizamos este padrão no nosso projeto sempre que queremos instanciar algum tipo de objeto.

#### **3.3.2-State**
É um padrão de desenho do tipo comportamental que permite que um objeto altere o seu comportamento quando o seu estado interno muda. Parece que o objeto modificou a sua classe. Utilizamos este padrão para facilitar a gestão do estado em que o animal se encontra.
Qualquer animal pertencente a uma associação pode ter um destes 3 estados :

- Adotado.
- Para adoção.
- Perdido.

#### **3.3.3-Singleton**
O singleton padrão de desenho do tipo criação que resolve 2 problemas ao mesmo tempo, assegura que a classe irá ter apenas uma instancia e fornecer um acesso global a essa instancia, no entanto viola o Principio da responsabilidade única.

Este padrão de desenho é utilizado em todas as factories, uma vez que só queremos instanciar uma única vez esse objeto.

#### **3.3.3-Template Method**
O padrão de desenho Template Method auxilia na definição de um algoritmo com partes do mesmo definidos por métodos abstratos. As suas sbuclasses devem ser responsáveis por estas partes abstratas, em que serão implementadas de várias formas, diferenciando um comportamento em cada subclasse. Este padrão fornece uma estrutura fixa, de um algoritmo, na qual está presente na superclasse.

Este padrão é utilizado em todos os controllers, em que permite a construção de diferentes respostas aos pedidos HTTP, ou seja para o frontend. 

#### **3.3.4-Single File Components**
O padrão Single File Components (SFC)é um padrão desenho utilizado no lado do cliente, ou seja este é utilizado na framework javascript Vuejs de modo a organizar e otimizar o desenvolvimento do mesmo.
O padrão referido anteriormente permite o isolamento de componentes garantido uma independência relativa entre HTML, CSS e JavaScript, desta forma é possível eliminar a instanciação de valores globais, reduzir a complexidade do código através de simplificação de HTML, suporte integrado de CSS, maximizar a liberdade de tecnologias de desenvolvimento, módulos CommonJS e limitações de CSS para componentes individuais.
Este é o padrão de desenvolvimento mais utilizado no lado do cliente, pois, pode ser implementado de forma consistente em cada componente, onde as secções de template, lógica e estilo são fortemente acoplados e a sua implementação torna o componente coesivo facilitando a sua manutenção [2].

#### **3.3.5-Functional components**
 De uma forma semelhante ao padrão anterior, este trata-se também de um padrão utilizado no lado do cliente que se refere a uma especialização dos SFC onde exclui a necessidade de um estado. Os benefícios desta tipologia de componentes são relativos à performance, nomeadamente nos tempos de renderização das páginas e otimização na utilização de memória.

Estes componentes descritos anteriormente possuem a particularidade de não necessitarem da secção responsável pela lógica, pois todos os dados necessários são transmitidos através de props e eventos provenientes de componentes de níveis distintos [2].

#### **3.3.6-Component Communication**
Finalmente para finalizar a secção relativa a padrões no lado do cliente, a Componente Communication permite uma comunicação eficiente entre componentes de níveis distintos, facilitando assim a reutilização de informação sem utilização de recursos desnecessários.

Os componentes VueJs seguem um protocolo de comunicação unilateral com componentes vizinhos, que consiste na utilização de eventos no sentido superior e props no sentido inferior. Estes últimos são dados reativos de leitura, ou seja componentes filhos não conseguem alterar os seus valores, mas quando estes alteram, os componentes filhos que os utilizam são novamente renderizados. Os componentes filhos poderão enviar pedidos de alteração de valores dos props através da emissão de eventos para o componente pai, de modo a que este altere os dados em questão mapeados no componente filho [2].

## **4-Utilização**
A plataforma web desenvolvida está diferenciada através de duas secções distintas, nomeadamente as páginas acessíveis para todos os visitantes
onde é possível encontrar todas as funcionalidades básicas e um painel administrativos para associações e administrador.

### **4.1- Painel administrativo**
Através do painel administrativo o administrador poderá executar as ações CRUD (criação, consulta, atualização e destruição de dados) sobre os utilizadores registados na plataforma, comparativamente as associações poderão executar estas mesmas ações CRUD sobre os animais, eventos e novidades pertencentes a esta associação em questão. Adicionalmente é ainda possível à associação atualizar o seu próprio perfil.
```
PRINTS
```
### **4.2- Animais**
Os animais inseridos na plataforma poderão ser acedidos em diversas páginas da interface. Ao explorar o website poderemos encontrar na página inicial uma listagem ”infinita” de todos os animais, de todas as associações ordenados por ordem de colocação na plataforma do mais recente. Esta abordagem foi escolhida de modo a captar de forma mais rápida possível a atenção do visitante.

Cada animal é apresentado em formato carta fornecendo assim individualização e independência. Adicionalmente poderemos ainda carregar em qualquer animal para visualizar dados mais detalhados como por exemplo peso, porte, idade, entre outros.

```
PRINTS
````
Poderão ainda ser visualizados animais pertencentes de uma dada associação através do perfil público dessa mesma associação, na página ”Amiguinhos” que será descrita numa sub-secção seguinte e numa tabela pertencente ao painel administrativo da associação em questão.

```
PRINTS
````

### **4.3- Eventos**
De forma semelhante à página principal descrita anteriormente, os eventos possuem também uma secção dedicada à sua apresentação. E, também, utilizada ́uma listagem ordenada por eventos com data de inicialização mais próxima, não sendo apresentados eventos que já tenham acontecido. Os eventos estão também presentes no perfil público duma dada associação e no seu painel administrativo.

```
PRINTS
````

### **4.4- Perdidos**
A página para animais perdidos é um pouco diferente das descritas anteriormente, pois esta tem como objetivo a inserção de dados por qualquer visitante da plataforma, não estando associada com qualquer tipo de utilizador ou associação.

A página é constituída por um formulário que qualquer utilizador poderá preencher, com o objetivo de dar como perdido um animal, onde são
fornecidos dados de contacto e descrições do animal em questão. Desta forma outros utilizadores que visitem a página poderão ajudar no que pode ser melhor caracterizado como ”perdidos & achados” baseado em animais domésticos.

```
PRINTS
````

### **4.5- Amiguinhos**
Para finalizar, esta última secção visa dinamizar e cativar uma grande vertente de utilizadores jovens. Aqui são apresentados de forma muito mais infantil e dinâmica os animais bebés (e.x ¡1 ano) com auxílio de animações e efeitos visuais.

## **5-Discussão**
Nesta secção iremos abordar as vantagens e as desvantagens da nossa aplicação web do ponto de vista da utilização do cliente.


### **5.1- Vantagens**
- Reúne todas as informações das principais associações de proteção animal num sítio só. O que permite aos utilizadores uma maior facilidade ao  navegar pelas associações.
- Acesso fácil a um catálogo de animais para adoção.
- Pré-visualização de animais.
- Facilidade ao publicar um post para um animal perdido.

### **5.2- Desvantagens**
- Não possui uma opção direta de doação a uma associação, mas fornecemos dados para tal.
-

## **6-Conclusão**

Como supramencionado, achamos que os principais critérios a ter em conta ao avaliar o produto final são as vantagens/desvantagens do ponto de vista do utilizador e, se a aplicação contribui para a solução do problema proposto.


O único problema que antecipamos é a possibilidade de existir utilizadores não familiarizados com aplicações informáticas que possuam dificuldades em utilizar o sistema. Esse problema não pode ser ,efetivamente, resolvido mas tentamos sempre minimiza-lo procurando simplificar ao máximo a interface da aplicação.



##  **References**
1. EF. 2020. Entity Framework Core. (2020).
    https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx.
2. Vue Patterns. 2020. Vue Patterns. (2020). https://learn-vuejs.github.io/vue-patterns/patterns/#component-declaration.
3. TVI24. 2019. Cerca de 50 mil animais abandonados são resgatados das ruas por-
    tuguesas por ano. (2019). https://tvi24.iol.pt/sociedade/abandono/cerca-de-50-mil-animais-abandonados-sao-recolhidos-das-ruas-portuguesas-por-ano.

## 7 Anexos
