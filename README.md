# unity-toolbox

Este repositório serve como um laboratório técnico (cookbook) de ferramentas de automação de produção, matemática aplicada e engenharia inversa de mecânicas de videojogos em Unity 3D. O foco principal é o isolamento de problemas de programação e a criação de soluções modulares e limpas.

## 🛠️ Tecnologias Utilizadas
* **Motor de Jogo:** Unity 3D
* **Linguagem:** C#
* **Metodologia:** MVP (Minimum Viable Product) e Padrões de Componentes

## 🔧 Projetos & Mecânicas Implementadas

### 1. Automação de Pipeline (`NoBackground Tool`)
* **O que faz:** Uma ferramenta utilitária para o editor que automatiza a produção de sprites a partir de modelos 3D de componentes de automóveis.
* **Lógica Técnica:** Manipulação programática da câmara, rotações incrementais precisas via matrizes de rotação e exportação automatizada para ficheiros `.png` com fundo transparente.

### 2. Matemática Aplicada e Física
* **GridTest:** Algoritmo que utiliza projeção de vetores e *Raycasting 3D* para detetar a colisão do rato com o ambiente e calcular instantaneamente em que quadrante de uma grelha lógica o utilizador está posicionado.
* **OrientationTest:** Sistema que spawna dinamicamente um número `X` de veículos numa pista, adaptando a orientação espacial e rotação (`Quaternion.LookRotation`) de cada um com base na inclinação do terreno.

### 3. Engenharia Inversa de Jogos (Clones & MVPs)
* **LanesRunner:** Protótipo funcional com a mecânica core de movimentação por faixas laterais (estilo *Subway Surfers*).
* **ShapesTest & SlasherTest:** Réplicas mecânicas de precisão de jogos comerciais (*Stack* da Ketchapp e *Dark Slash* da Veewo Games) para validação de game loops rápidos.
* **Skyrim Lockpicking & Keypad:** Sistema de minijogos de interação baseados em colisão de Raycast e deteção de ângulos.

### 4. Persistência de Dados (`SaveLoadTest`)
* **O que faz:** Sistema modular para gravação e leitura de progresso de jogo através de ficheiros encriptados localmente, garantindo a integridade dos dados guardados.
