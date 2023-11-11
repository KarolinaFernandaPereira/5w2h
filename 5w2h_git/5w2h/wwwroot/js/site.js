
const $modal = document.getElementById('modal');

const $idTarefa = document.getElementById('idTarefa');
const $idQuadroTarefa = document.getElementById('codigoQuadro1');

const $quadros = document.getElementById('quadros');


const $novosQuadros = document.getElementById('novosQuadros');

const $modalQuadro = document.getElementById('modalQuadro');
const $nomeQuadro = document.getElementById('nomeQuadro');
const $idQuadro = document.getElementById('idQuadro');


const colunas = [1, 2, 3];

var idVerTarfa = 0;

//Criação Tarefa
const $nomeTarefa = document.getElementById('nomeTarefa')
const $descricao = document.getElementById('descricao');
const $oque = document.getElementById('oque')
const $onde = document.getElementById('onde')
const $quando = document.getElementById('quando')
const $como = document.getElementById('como')
const $quanto = document.getElementById('quanto')
const $quem = document.getElementById('quem')
const $porque = document.getElementById('porque')


const $createbutton = document.getElementById('createButton')
const $createMode = document.getElementById('createMode')

const $editButton = document.getElementById('editButton')
const $inativarButton = document.getElementById('inativarButton')
const $editMode = document.getElementById('editMode')

const $createQuadroButton = document.getElementById('createQuadroButton')
const $controleTarefa = document.getElementById('controleTarefa')
const $controleQuqadro = document.getElementById('controleQuadro')
const $createModeQuadro = document.getElementById('createModeQuadro')

const $iconeViewTarefa = document.getElementById('viewTarefaIcone');
const $modeViewTarefa = document.getElementById('viewTarefaMode')


const $adicionarMemrboLogo = document.getElementById('adicionarMemrboLogo')
const $spanAdicionarMembro = document.getElementById('spanAdicionarMembro')
const $logoSemMembros = document.getElementById('logoSemMembros')
const $spanSemMembros = document.getElementById('spanSemMembros')

const $openSideMenu = document.getElementById('openSideMenu')
const $menuPerfilMobile = document.getElementById('modalPerfilMobile')

let dataColumn = 4

$menuPerfilMobile.addEventListener('click', function (e) {
    if (e.target == this) fecharPerfilMobile();
});

function openPerfilMobile() {
    $menuPerfilMobile.classList.add("hide");
}

function fecharPerfilMobile() {
    $menuPerfilMobile.classList.remove("hide");
}


var quadrosLsta = [];

var listaTarefa = [];

var tarefaMembros = []

var colunaTarefa;

function teste1(id) {
    console.log(id);
}

//função para abrir o modal, on muda a propriedade flex para fazer ele aparecer
function openModal(data_column) {

    $modal.style.display = "flex";

    //Aqui verificamos se ele é passado o Id para o modal pois se sim ele será aberto no modo de Editar a tarefa
    colunaTarefa = data_column
    $idQuadroTarefa.value = colunaTarefa
    

    $createMode.style.display = "block";
    $createbutton.style.display = "block";

    $editButton.style.display = "none";
    $inativarButton.style.display = "none"
    $editMode.style.display = "none";

    $iconeViewTarefa.style.display = "none"
    $modeViewTarefa.style.display = "none"

   

    $logoSemMembros.style.display = "none"
    $spanSemMembros.style.display = "none"

    $descricao.disabled = false
    $nomeTarefa.disabled = false
    $oque.disabled = false
    $onde.disabled = false
    $quando.disabled = false
    $como.disabled = false
    $quanto.disabled = false
    $quem.disabled = false
    $porque.disabled = false

    
}


var idDelete = 0
var quadroIdBase = 0
var quadroID = 0

var quantidadeQuadros = 3;

const $editDinamicoQuadro = document.getElementById('editDinamico');
const $deleteDinamico = document.getElementById('deleteDinamico');
const $editmodeQuadro = document.getElementById('editmodeQuadro');

const $idUpdateQuadro = document.getElementById('idUpdateQuadro');
const $idIntivarQuadro = document.getElementById("idInativarQuadro");

function openModalQuadroEditDinamico(id) {
    $modalQuadro.style.display = "flex";
    $controleQuqadro.style.display = "block flex"
    $createQuadroButton.style.display = "none";
    $createModeQuadro.style.display = "none";

    $editmodeQuadro.style.display = "block"

    $editDinamicoQuadro.style.display = "block";
    $deleteDinamico.style.display = "block";



    const indexQuadro = quadrosLsta.findIndex(function (base) {
        return base.id == id;
    });
    const quadroDinamico = quadrosLsta[indexQuadro]


    
    $idUpdateQuadro.value = id;
    $idIntivarQuadro.value = id;
    console.log($idUpdateQuadro.value)

    $nomeQuadro.value = quadroDinamico.nomeQuadro
    quadroID = quadroDinamico.id
}

const $idTarefaUpdate = document.getElementById('idUpdateTarefa');
const $idInativarTarefa = document.getElementById('idInativarTarefa');

function openModalToEdit(id, quado_codigo) {

    $modal.style.display = "flex";
    $idTarefaUpdate.value = id;
    console.log($idTarefaUpdate.value)

    $idQuadroTarefa.value = quado_codigo
    $idInativarTarefa.value = id;


    $createMode.style.display = "none";
    $createbutton.style.display = "none";

    $editMode.style.display = "block";
    $editButton.style.display = "block";
    $inativarButton.style.display = "block"
    

    $iconeViewTarefa.style.display = "none";
    $modeViewTarefa.style.display = "none";

    $adicionarMemrboLogo.style.display = "block"
    $spanAdicionarMembro.style.display = "block"

    $logoSemMembros.style.display = "none"
    $spanSemMembros.style.display = "none"
    const $membrosTarefa = document.getElementById('novosMembrosTarefa')
    
    $.ajax({
        type: "get", // Ou "GET", dependendo da sua necessidade
        url: "Gestaoo/teste/editarTarefa", // Substitua pelos valores corretos
        data: { idT: id}, // Passa a variável como parte da solicitação
        success: function (response) {
            // Manipule a resposta do controlador, se houver
            console.log(response)
            console.log(response.tarefa.nome)
            var teste = new Date(response.tarefa.quando)
            console.log(teste)
            teste = teste.toLocaleDateString('pt-BR').toString()

            var dia = teste.split("/")[0];
            var mes = teste.split("/")[1];
            var ano = teste.split("/")[2];

            var formatDate = ano + '-' + ("0" + mes).slice(-2) + '-' + ("0" + dia).slice(-2);
            console.log(formatDate)

            $nomeTarefa.value = response.tarefa.nome
            $descricao.value = response.tarefa.descricao
            $oque.value = response.tarefa.oque
            $onde.value = response.tarefa.onde
            $quando.value = formatDate
            $como.value = response.tarefa.como
            $quanto.value = response.tarefa.quanto
            $quem.value = response.tarefa.quem
            $porque.value = response.tarefa.porQue

            response.tarefa.funcionario.map(function (membro) {
                const novoMembro = `
                    <div class="membroAdicionado" style="display: flex;
                                                        flex-direction: column;
                                                        align-items: center;
                                                        padding-right: 22px;
                                                        padding-top: 4px;
                                                        padding-bottom: 4px;" >
                    
                        <ion-icon name="person-circle-outline" style="
                        font-size: 50px;"></ion-icon>
                    
                        <span style="text-align: center;">${membro.nomeCompleto}</span>
                    
                    </div>
            
                `

                $membrosTarefa.innerHTML += novoMembro
            })
        },
        error: function (error) {
            // Trate erros, se houver
            console.log("Erro: " + error);
        }
    });

    idDelete = tarefa.id
    colunaTarefa = tarefa.coluna
    tarefaMembros = tarefa.membros
    
    tarefa.membros.forEach(function (membro) {
        const novoMembro = `
                <div class="membroAdicionado" style="display: flex;
                                                    flex-direction: column;
                                                    align-items: center;
                                                    padding-right: 22px;
                                                    padding-top: 4px;
                                                    padding-bottom: 4px;" >
                    
                    <ion-icon name="person-circle-outline" style="
                    font-size: 50px;"></ion-icon>
                    
                    <span style="text-align: center;">${membro}</span>
                    
                </div>
            
            `

        $membrosTarefa.innerHTML += novoMembro
    });


}


//Função para fechar o modal
function closeModal() {
    $modal.style.display = "none";

    $idTarefa.value = "",
        $nomeTarefa.value = "",
        $descricao.value = "",
        $oque.value = "",
        $onde.value = "",
        $quando.value = "",
        $como.value = "",
        $quanto.value = "",
        $quem.value = "",
        $porque.value = ""

    const $membrosTarefa = document.getElementById('novosMembrosTarefa')
    $membrosTarefa.innerHTML = "";
    tarefaMembros = []
}


function dragstart_handler(ev) {

    ev.dataTransfer.setData("my_customn_data", ev.target.id);
    ev.dataTransfer.effectAllowed = "move";
}

function dragover_handler(ev) {
    ev.preventDefault();
    ev.dataTransfer.dropEffect = "move";
}

function drop_handler(ev) {
    ev.preventDefault();

    const data = ev.dataTransfer.getData("my_customn_data");
    var coluna_id = ev.target.dataset.column;
    coluna_id = parseInt(coluna_id)


    trocar_coluna(data, coluna_id);
}



function trocar_coluna(tarefa_id, coluna_id) {

    $(document).ready(function () {
        // Sua variável JavaScript que deseja enviar
        var tarefaId = tarefa_id;
        var colunaId = coluna_id;

        // Fazer a solicitação AJAX
        $.ajax({
            type: "POST", // Ou "GET", dependendo da sua necessidade
            url: "Gestaoo/teste/TrocarColuna", // Substitua pelos valores corretos
            data: { tarefaId: tarefaId, colunaId: coluna_id }, // Passa a variável como parte da solicitação
            success: function (response) {
                // Manipule a resposta do controlador, se houver
                
                window.location.href = '/Gestaoo'
            },
            error: function (error) {
                // Trate erros, se houver
                console.log("Erro: " + error);
            }
        });
    });
    
}

function trocar_colunaHome(tarefa_id, coluna_id) {

    $(document).ready(function () {
        // Sua variável JavaScript que deseja enviar
        var tarefaId = tarefa_id;
        var colunaId = coluna_id;

        // Fazer a solicitação AJAX
        $.ajax({
            type: "POST", // Ou "GET", dependendo da sua necessidade
            url: "Gestaoo/teste/TrocarColuna", // Substitua pelos valores corretos
            data: { tarefaId: tarefaId, colunaId: coluna_id }, // Passa a variável como parte da solicitação
            success: function (response) {
                // Manipule a resposta do controlador, se houver
                
                window.location.href = '/'
            },
            error: function (error) {
                // Trate erros, se houver
                console.log("Erro: " + error);
            }
        });
    });
    
}
var quantidadeAdicionada = 0;




    




function renderizarColunas(a) {
    
   
    a.forEach(function (a) {
        const bodyColuna = document.querySelector(`[data-column="${a.CodigoQuadro}"] .body .cards_list`);

        const task = `
                <div id="${a.Codigo}" class="card"  draggable="true" 
                ondragstart="dragstart_handler(event)">
                    <div class="info" style="display: flex; justify-content: space-between; align-items: center;}" ondblclick="visualizarTarefa(${a.Codigo})">
                        <b style="max-width: 6rem;
                        flex-wrap: nowrap;
                        word-wrap: break-word;"> ${a.Nome}</b>
                        <div id="membrosCloseTarefa_${a.Codigo}" style="display: none"></div>
                        <div id="vierEditarTarefa_${a.Codigo}">


                                <ion-icon name="create-outline" style="cursor: pointer; border-radius: 0;align-items: center;
                                display: flex; font-size: 21px
                                " onclick="openModalToEdit(${a.Codigo})" title="Editar"></ion-icon>
                            
                        </div>
                    </div>
                    <span id="mais${a.Codigo}" class="mais">
                    <ul>
                        <div class="info">
                            <b class="info" title="${a.Oque}" style="cursor: pointer;">O Quê?</b>
                            
                            <b class="info" title="${a.Onde}" style="cursor: pointer;">Onde ?</b>
                        
                            <b class="info" title="${a.Quando}" style="cursor: pointer;">Quando ?</b>
                        
                            <b class="info" title="${a.Como}ê" style="cursor: pointer;">Como ?</b>
                        
                            <b class="info" title="${a.Quanto}" style="cursor: pointer;">Quanto ?</b>
                        
                            <b class="info" title="${a.Quem}" style="cursor: pointer;">Quem ?</b>
                        
                            <b class="info" title="${a.Porque}" style="cursor: pointer;">Por Quê ?</b>
                        </div>
                    </ul>
                    </span>

                    <button class="verMais" id="btnVerMais_${a.Codigo}" onclick="verMais(${a.Codigo})">
                        <ion-icon name="arrow-down-circle-outline"></ion-icon>
                    </button>

                    <button class="verMenos" id="btnVerMenos_${a.Codigo}" onclick="verMais(${a.Codigo})">
                        <ion-icon name="arrow-up-circle-outline"></ion-icon>
                    </button>
                </div> 
            `;

        bodyColuna.innerHTML += task;



    });

    listaTarefa.forEach(function (tarefa) {
        const $btnVerMenos = document.getElementById('btnVerMenos_' + tarefa.id);

        $btnVerMenos.style.display = "none";

    });



}


//Função usada pra criar as tarefas
function openViewTarefa(id) {
    const $modalVerTarefa = document.getElementById('viewTarefa_' + id)


    if ($modalVerTarefa.style.display === 'flex') {
        $modalVerTarefa.style.display = 'none'
    } else {
        $modalVerTarefa.style.display = 'flex'

    }

    if ($modal.style.display === "flex") {
        $modalVerTarefa.style.display = 'none'
    }

}


var primeirosMembros = []
const $idVisualizarTarefa = document.getElementById('idVisualizar');

function visualizarTarefa(id) {
    $modal.style.display = "flex";
    $idVisualizarTarefa.value = id

    $createMode.style.display = "none";
    $createbutton.style.display = "none";

    $editMode.style.display = "none";
    $editButton.style.display = "none";

    

    $iconeViewTarefa.style.display = "block"
    $modeViewTarefa.style.display = "block"

    $adicionarMemrboLogo.style.display = "none"
    $spanAdicionarMembro.style.display = "none"

    //Parte do código onde percorremos a lista htmk até achar um id igual o da tarefa que foi passada
    //spanAdicionarMembro
    //spanSemMembros
    /*logoSemMembros
    spanSemMembros*/

    const index = listaTarefa.findIndex(function (tarefa) {
        return tarefa.id == id;
    });
    const tarefa = listaTarefa[index]

    $idTarefa.value = tarefa.id,
        $descricao.value = tarefa.descricao,
        $nomeTarefa.value = tarefa.nome,
        $oque.value = tarefa.oque,
        $onde.value = tarefa.onde,
        $quando.value = tarefa.quando,
        $como.value = tarefa.como,
        $quanto.value = tarefa.quanto,
        $quem.value = tarefa.quem,
        $porque.value = tarefa.porque

    $descricao.disabled = true
    $nomeTarefa.disabled = true
    $oque.disabled = true
    $onde.disabled = true
    $quando.disabled = true
    $como.disabled = true
    $quanto.disabled = true
    $quem.disabled = true
    $porque.disabled = true

    idDelete = tarefa.id
    colunaTarefa = tarefa.coluna


    const $membrosTarefa = document.getElementById('novosMembrosTarefa')
    if (tarefa.membros.length === 0) {
        $logoSemMembros.style.display = "block"
        $spanSemMembros.style.display = "block"

    } else {
        $logoSemMembros.style.display = "none"
        $spanSemMembros.style.display = "none"

        tarefa.membros.forEach(function (membro) {
            const novoMembro = `
                    <div class="membroAdicionado" style="display: flex;
                                                        flex-direction: column;
                                                        align-items: center;
                                                        padding-right: 22px;
                                                        padding-top: 4px;
                                                        padding-bottom: 4px;" >
                        
                        <ion-icon name="person-circle-outline" style="
                        font-size: 50px;"></ion-icon>
                        
                        <span style="text-align: center;">${membro}</span>
                        
                    </div>
                
                `

            $membrosTarefa.innerHTML += novoMembro
        });
    }

}



//Função usada para a edição de tarefa

function updateTarefa() {


    //Cria uma const com os valores da variavel para poder inserir no html
    const Tarefa = {
        id: $idTarefa.value,
        nome: $nomeTarefa.value,
        descricao: $descricao.value,
        oque: $oque.value,
        onde: $onde.value,
        quando: $quando.value,
        como: $como.value,
        quanto: $quanto.value,
        quem: $quem.value,
        porque: $porque.value,
        coluna: colunaTarefa,
        membros: []
    }

    //Procura o index da tarefa para poder achar qual tarefa será editada 
    const index = listaTarefa.findIndex(function (tarefa) {
        Tarefa.membros = tarefa.membros;
        return tarefa.id == $idTarefa.value;
    });



    //Adiciona a nova tarefa na lista
    listaTarefa[index] = Tarefa



    closeModal();

    renderizarColunas();
}

function updateQuadro() {
    const Quadro = {
        id: $idQuadro.value,
        nomeQuadro: $nomeQuadro.value
    }

    const headColuna = document.querySelector(`[data-column="${$idQuadro.value}"] .head span`);
    headColuna.textContent = $nomeQuadro.value

    const indexQuadro = baseQuadro.findIndex(function (quadroB) {
        return quadroB.id == $idTarefa.value;
    });

    baseQuadro[indexQuadro] = Quadro
    closeModalQuadro()

}

const $modalDelete = document.getElementById('modalDelete')
const $modalDeleteQuadro = document.getElementById('modalDeleteQuadro')


function openModalDelete() {
    $modalDelete.style.display = "flex";


}

function openModalDeleteQuadro() {
    $modalDeleteQuadro.style.display = "flex";


}
function closeModalDeleteQuadro() {
    $modalDeleteQuadro.style.display = "none";


}

function closeModalDelete() {
    $modalDelete.style.display = "none";
}

function deleteTarefa() {



    const index = listaTarefa.findIndex(function (tarefa) {
        return tarefa.id == idDelete
    })



    listaTarefa.splice(index, 1);
    console.log(listaTarefa)

    closeModalDelete();
    closeModal()
    renderizarColunas();
}

//Função que comando o funcionamento do botão de ver mais
function verMais(id) {
    //Pega o span com id mais e verifica seru display
    const $maisTexto = document.getElementById("mais_" + id);
    const $verMenos = document.getElementById("btnVerMenos_" + id)
    const $verMais = document.getElementById("btnVerMais_" + id)
    const $divControleTarefa = document.getElementById("vierEditarTarefa_" + id)
    const $membrosTarefaClose = document.getElementById('membrosCloseTarefa_' + id)

    const index = listaTarefa.findIndex(function (tarefa) {
        return tarefa.id == id
    })

    const closeTarefa = listaTarefa[index]

    // Ou seja se o display for inline que dizer que o texto está aparecendo 
    if ($maisTexto.style.display === 'inline') {

        // Então no clique do botão ele muda para none escondendo o text
        $maisTexto.style.display = 'none'
        $verMais.style.display = "block";
        $verMenos.style.display = "none";
        $divControleTarefa.style.display = "block"
        $membrosTarefaClose.style.display = "none"
        $membrosTarefaClose.innerHTML = ""

    } else {
        // e Caso ele não seja inline que dizer que já está escondido então ele mostra
        $maisTexto.style.display = 'inline';
        $verMenos.style.display = "block";
        $verMais.style.display = "none";
        $divControleTarefa.style.display = "none"
        $membrosTarefaClose.style.display = "block flex"

       
   
       
              
    }
}




//Função utilizada para criar um novo quadro
//Ainda não está 100%


function criarQuadro() {
    /* Aqui temos a const usada para criar uma nova tarefa
        id = Gerado de maneira aleatoria
        descricao, prioridade, prazo final = Pega o valor da variavel
    */


    const novoQuadro = {
        id: dataColumn,
        nomeQuadro: $nomeQuadro.value
    }


    //Adicionado a lista de html
    quadrosLsta.push(novoQuadro);
    quantidadeQuadros++;

    colunas.push(dataColumn)




    closeModalQuadro();
    renderizarColunasQuadros();
    renderizarColunas();
    dataColumn++;
}

function OpenModalQuadro() {
    $modalQuadro.style.display = "flex";
    $createQuadroButton.style.display = "block";
    $controleQuqadro.style.display = "none";
    $editmodeQuadro.style.display = "none"
    $createModeQuadro.style.display = "block"
}

function closeModalQuadro() {
    $modalQuadro.style.display = "none";
    $nomeQuadro.value = "";


}



const $perfil = document.getElementById('modalPerfil');

function openPerfil() {
    if ($perfil.style.display === "flex") {
        $perfil.style.display = "none"
    } else {

        $perfil.style.display = "flex";
    }
}

function fecha() {
    $perfil.style.display = 'none';
}

$perfil.addEventListener('click', function (e) {
    if (e.target == this) fecha();
});












function resetarColunasDinamicas() {


    colunas.map((column) => {
        document.querySelector(`[data-column="${column}"] .body .cards_list`).innerHTML = '';
    })
}


function renderizarColunasQuadros() {
    //Percorrendo a lista de obetos para gerar uma de html
    //Html gerado para acriação do card de tarefa
    //Mexendo no html abaixo o card de tarefa muda

    $quadros.innerHTML = ""
    quadrosLsta.forEach(function (quadro) {
        /* Dentro da <span id=mais> fica a parte do texto que será escondida
        no caso acho que onde será colocar os 5w2h no momento está repetindo o prazo final */

        const quadroNovo = `
        
        <div class="column" id="${quadro.nomeQuadro.toLowerCase()}" data-column="${quadro.id}" 
        ondrop="drop_handler(event)" ondragover="dragover_handler(event)">
            <div class="head">
                <span>${quadro.nomeQuadro}</span>
                <ion-icon name="ellipsis-horizontal" id="headIcon" onclick="openModalQuadroEditDinamico(${quadro.id})"></ion-icon>
            </div>
            <div class="body">
                <div class="cards_list"></div>
                <button class="add_btn" onclick="openModal(${quadro.id})">Adicionar Tarefa</button>
            </div>
        </div>

        `;
        $quadros.innerHTML += quadroNovo






    });


}



function updateQuadroDinamico() {

    const Quadro = {
        id: parseInt($idQuadro.value),
        nomeQuadro: $nomeQuadro.value
    }


    const indexQuadro = quadrosLsta.findIndex(function (quadroB) {
        return quadroB.id == $idQuadro.value;
    });


    quadrosLsta[indexQuadro] = Quadro

    closeModalQuadro()
    renderizarColunasQuadros()
    renderizarColunas()
}


function deleteQuadroDinamico() {

    const index = quadrosLsta.findIndex(function (quadro) {
        return quadro.id == quadroID
    })

    

    quadrosLsta.splice(index, 1);
    quantidadeQuadros--;
    dataColumn--;
    colunas.splice(index, 1)

    listaTarefa = listaTarefa.filter(tarefa => tarefa.coluna != quadroID)
    console.log(quadrosLsta)
    console.log(colunas)


    closeModalDeleteQuadro()
    closeModalQuadro()
    renderizarColunasQuadros()
    renderizarColunas();
}

function showThumbnail(filess) {
    var url = filess.value;
    var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
    if (filess.files && filess.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
        var reader = new FileReader();
        reader.onload = function (e) {
            const img = document.getElementById('fotoCapa').setAttribute('src', e.target.result);
            img.classList.add("fotoTrocada")
        }


        reader.readAsDataURL(filess.files[0]);
    }
}

const $modalNovoMembro = document.getElementById('adicionarMembro')
var teste = []
var membros = []

const $membrosOcultos = document.getElementById('membrosOcultos')
function adicionarMembro() {
    
    const $membrosTarefa = document.getElementById('novosMembrosTarefa')
    var nomeMembro = document.getElementById('novoMembro')
    
    const novoMembro = `
        <div class="membroAdicionado" style="display: flex;
                                            flex-direction: column;
                                            align-items: center;
                                            padding-right: 22px;
                                            padding-top: 4px;
                                            padding-bottom: 4px;" >
            
            <ion-icon name="person-circle-outline" style="
            font-size: 50px;"></ion-icon>
            
            <span style="text-align: center;">${nomeMembro.value}</span>
            
        </div>
        
    `

    $membrosTarefa.innerHTML += novoMembro

    membros.push(nomeMembro.value)
    $membrosOcultos.innerHTML += nomeMembro.value + " "

    console.log($membrosOcultos.innerHTML)
    
    closeModalMembro();
}

function showModalMembro() {
    $modalNovoMembro.style.display = "flex"

}

function closeModalMembro() {
    $modalNovoMembro.style.display = "none"

}

