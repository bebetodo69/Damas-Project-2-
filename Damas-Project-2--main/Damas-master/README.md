## Mestrado profissional em Desenvolvimento de Jogos Digitais (PUC-SP)

<p align="center">
<img class="mestrado" src="https://raw.githubusercontent.com/ezefranca/Damas/master/logo_mestrado_new.png" width="30%" height="30%">
</p>

# Damas
### Disciplina: Laboratório 2 
### Professor Dr. Reinaldo Augusto de Oliveira Ramos

![](https://raw.githubusercontent.com/ezefranca/Damas/master/print.png?token=AA32WUH44VEAP5LEVW4EW3C6QKGZS)

## Desafio

Balancear a "IA" de modo que o jogador não perceba se é uma IA ou jogador. **Sem utilização de algoritmos de inteligência artificial.**

## Solução

O tabuleiro foi organizado como uma matriz, onde cada tipo de peça foi representado por um numero (1 e 2) e os espaços por 0.

                [ 0 1 0 1 0 1 0 1 ]
                [ 1 0 1 0 1 0 1 0 ]     
    Tabuleiro = [ 0 0 0 0 0 0 0 0 ]    
                [ 0 0 0 0 0 0 0 0 ]
                [ 0 0 0 0 0 0 0 0 ]
                [ 0 0 0 0 0 0 0 0 ]
                [ 0 2 0 2 0 2 0 2 ]
                [ 2 0 2 0 2 0 2 0 ]

### Ordem de decisão

1 Verificamos os vizinhos adjacentes na matriz, para indentificar uma possível morte do oponente. 
Exemplo: A peça marcada com **X** neste caso seria a primeira escolha


                [ 0 1 0 1 0 1 0 1 ]
                [ X 0 1 0 1 0 1 0 ]     
    Tabuleiro = [ 0 2 0 0 0 0 0 0 ]    
                [ 0 0 0 0 0 0 0 0 ]
                [ 0 0 0 0 0 0 0 0 ]
                [ 0 0 0 0 0 0 0 0 ]
                [ 0 0 0 2 0 2 0 2 ]
                [ 2 0 2 0 2 0 2 0 ]

2 Escolha randomica, dependendo da possíbilidade de movimento.
 
 ## Resultados
 
 Vídeo no Youtube:
 
 [![Foo](https://raw.githubusercontent.com/ezefranca/Damas/master/thumb.png?token=AA32WUDAPAI3HARMVCZXULS6QKJY)](https://www.youtube.com/watch?v=IHKuQMgCsxs&feature=youtu.be)

