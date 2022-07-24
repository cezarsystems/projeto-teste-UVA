// Constantes de configuração da chamada de API do cadastro de usuários
const port = 50340;
const baseUrl = "http://localhost:" + port + "/api";

// Função de seleção de div's da Tab
function openTabCadastroUsuario(evt, tabNome) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");

    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    tablinks = document.getElementsByClassName("tablinks");

    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace("active", "");
    }

    document.getElementById(tabNome).style.display = "block";
    evt.currentTarget.className += " active";
}

// Carrega todos os usuários cadastrados
function carregarUsuarios() {
    document.getElementById('qtdRegistros').style.visibility = 'hidden';
    document.getElementById('lblSemRegistroTabInicial').style.visibility = 'hidden';

    $("#tabelaRegistroGeral").empty();

    let url = baseUrl.concat("/usuarios");

    $.get(url, function (data) {
        if (data.length > 0) {
            let cabecalhoTabela =
                '<thead>' +
                '<tr>' +
                '<th scope="col">Nome</th>' +
                '<th scope="col">CPF</th>' +
                '<th scope="col">Telefone</th>' +
                '<th scope="col">E-mail</th>' +
                '<th scope="col">Data do Cadastro</th>' +
                '<th scope="col">Ativo</th>' +
                '<th scope="col">Visualizar</th>' +
                '<th scope="col">Editar</th>' +
                '<th scope="col">Excluir</th>' +
                '</tr>' +
                '</thead>' +
                '<tbody> ' +
                '</tbody>';

            $("#tabelaRegistroGeral").append(cabecalhoTabela);

            for (let i = 0; i < data.length; i++) {
                let dataFormatada = "";

                if (data[i].DataCriacao) {
                    let dataRetornada = new Date(data[i].DataCriacao);
                    dataFormatada =
                        dataRetornada.getDate() + "/" +
                        dataRetornada.getMonth() + "/" +
                        dataRetornada.getFullYear() + " às " +
                        dataRetornada.getHours() + ":" +
                        dataRetornada.getMinutes() + "hrs"
                } else {
                    dataFormatada = "N/D";
                }

                let linhaTabela =
                    "<tr>" +
                    "<td>" + data[i].NomeUsuario + "</td>" +
                    "<td>" + cpfMascarado(data[i].CPFUsuario) + "</td>" +
                    "<td>" + telefoneMascarado(data[i].TelefoneUsuario) + "</td>" +
                    "<td>" + data[i].EmailUsuario + "</td>" +
                    "<td>" + dataFormatada + "</td>" +
                    "<td>" + (data[i].Ativo == true ? "Sim" : "Não") + "</td>" +
                    "<td>" +
                    '<button type="button" class="btn btn-info btn-sm" onclick="openModalDetalhamentoUsuario(' + data[i].CodigoUsuario + ')">Detalhes</button>' +
                    "</td>" +
                    "<td>" +
                    '<button type="button" class="btn btn-warning btn-sm" onclick="openModalEdicaoUsuario(' + data[i].CodigoUsuario + ')">Editar</button>' +
                    "</td>" +
                    "<td>" +
                    '<button type="button" class="btn btn-danger btn-sm" onclick="openModalExclusaoUsuario(' + data[i].CodigoUsuario + ')">Excluir</button>' +
                    "</td>" +
                    "</tr>";

                $("#tabelaRegistroGeral tbody").append(linhaTabela);

                let labelQtdRegistros = document.getElementById('qtdRegistros');
                labelQtdRegistros.innerText = "Registro(s) encontrado(s): " + data.length;
                labelQtdRegistros.style.visibility = 'visible';
            }
        } else {
            document.getElementById('qtdRegistros').style.visibility = 'hidden';
            document.getElementById('lblSemRegistroTabInicial').style.visibility = 'visible';
        }
    }).fail(function () {
        $("#modalErro").modal().show();
    });
}

// Modal de detalhamento do usuário
function openModalDetalhamentoUsuario(codigoUsuario) {
    if (codigoUsuario > 0) {
        let url = baseUrl.concat("/usuario/" + codigoUsuario);

        let dadosUsuario = null;

        $.get(url, function (data) {
            if (data) {
                dadosUsuario = data;

                // Seta o nome do usuário
                $("#txtNome").val(dadosUsuario.NomeUsuario);

                // Seta o E-mail do usuário
                $("#txtEmail").val(dadosUsuario.EmailUsuario);

                // Seta o CPF do usuário
                $("#txtCPF").val(cpfMascarado(dadosUsuario.CPFUsuario));

                // Seta o Telefone do usuário
                $("#txtTelefone").val(telefoneMascarado(dadosUsuario.TelefoneUsuario));

                // Seta a Data de criação do usuário
                if (dadosUsuario.DataCriacao) {
                    let dataRetornada = new Date(dadosUsuario.DataCriacao);
                    dataFormatada =
                        dataRetornada.getDate() + "/" +
                        dataRetornada.getMonth() + "/" +
                        dataRetornada.getFullYear();
                } else {
                    dataRetornada = "N/D";
                }

                $("#txtDataCadastro").val(dataFormatada);

                if (dadosUsuario.Ativo == true)
                    $("#chkAtivo").prop("checked", true);
                else
                    $("#chkAtivo").prop("checked", false);

                $("#modalDetalhesUsuario").modal().show();
            } else {
                $("#modalSemDados").modal().show();
            }
        }).fail(function () {
            $("#modalErro").modal().show();
        });
    }
}

// Modal de edição do usuário
function openModalEdicaoUsuario(codigoUsuario) {
    $('#modalEdicaoUsuario').modal({backdrop: 'static', keyboard: false});

    if (codigoUsuario > 0) {
        let url = baseUrl.concat("/usuario/" + codigoUsuario);

        let dadosUsuario = null;

        $.get(url, function (data) {
            if (data) {
                dadosUsuario = data;

                // Seta o nome do usuário
                $("#txtEdicaoNome").val(dadosUsuario.NomeUsuario);

                // Seta o E-mail do usuário
                $("#txtEdicaoEmail").val(dadosUsuario.EmailUsuario);

                // Seta o CPF do usuário
                $("#txtEdicaoCPF").mask("000.000.000-00");
                $('#txtEdicaoCPF').val(cpfMascarado(dadosUsuario.CPFUsuario)).trigger('input');      

                // Seta o Telefone do usuário
                $("#txtEdicaoTelefone").mask("(00) 0000-0000");
                $('#txtEdicaoTelefone').val(dadosUsuario.TelefoneUsuario).trigger('input');               
                
                // Seta a Data de criação do usuário
                if (dadosUsuario.DataCriacao) {
                    let dataRetornada = new Date(dadosUsuario.DataCriacao);
                    dataFormatada =
                        dataRetornada.getDate() + "/" +
                        dataRetornada.getMonth() + "/" +
                        dataRetornada.getFullYear();
                } else {
                    dataRetornada = "N/D";
                }

                $("#txtEdicaoDataCadastro").val(dataFormatada);

                if (dadosUsuario.Ativo == true)
                    $("#chkEdicaoAtivo").prop("checked", true);
                else
                    $("#chkEdicaoAtivo").prop("checked", false);

                $("#modalEdicaoUsuario").modal().show();
            } else {
                $("#modalSemDados").modal().show();
            }
        }).fail(function () {
            $("#modalErro").modal().show();
        });

        $("#frmEdicaoUsuario").validate({
            rules: {
                txtEdicaoNome: {
                    required: true,
                    minlength: 5
                },
                txtEdicaoEmail: {
                    required: true,
                    email: true
                },
                txtEdicaoCPF: {
                    required: true,
                    minlength: 11,
                    cpfValido: true
                },
                txtEdicaoTelefone: {
                    required: true,
                    minlength: 14 
                }
            }, messages: {
                txtEdicaoNome: {
                    required: "Informe o nome completo do usuário",
                    minlength: "O nome do usuário deve conter pelo menos 5 caracteres"
                },
                txtEdicaoEmail: {
                    required: "Informe o um e-mail válido",
                    email: "Não foi informado um e-mail válido"             
                },
                txtEdicaoCPF: {
                    required: "Informe o CPF do usuário",
                    minlength: "Mínimo e máximo para um CPF válido são 11 dígitos",
                    cpfValido: "O CPF informado é inválido"
                },
                txtEdicaoTelefone: {
                    required: "Informe o telefone do usuário",
                    minlength: "Informar os 10 dígitos do número do telefone"
                }
            }
        });
    
        // Adiciona a validação do CPF ao validator
        jQuery.validator.addMethod("cpfValido", function(value) {
            return cpfValido(value);
        });

        $('#btnSalvarEdicao').unbind("click").bind("click", function () {
            let formularioEdicaoValido = $("#frmEdicaoUsuario").valid();

            if (formularioEdicaoValido) {
                // Dados do usuário
                const dadosPessoa = {
                    CodigoUsuario: codigoUsuario,
                    NomeUsuario: $("#txtEdicaoNome").val(),
                    EmailUsuario: $("#txtEdicaoEmail").val(), 
                    CPFUsuario: $("#txtEdicaoCPF").unmask().val(),
                    TelefoneUsuario: $("#txtEdicaoTelefone").unmask().val(),
                    Ativo: $("#chkEdicaoAtivo").prop('checked')
                };    

                // Limpa os dados do formulário
                $('#frmEdicaoUsuario')[0].reset();
                $("#frmEdicaoUsuario").validate().resetForm();  

                let urlPut = baseUrl.concat("/usuario/atualizar/");

                $.ajax({
                    url: urlPut,
                    method: 'PUT',
                    contentType: 'application/json',
                    data: JSON.stringify(dadosPessoa),
                    success: function (result) {
                        if (result == true) {
                            fecharModalEdicaoUsuario();
                            $("#modalSucessoEdicao").modal().show();
                            carregarUsuarios();
                        } else {
                            $("#modalErro").modal().show();
                        }
                    },
                    error: function () {
                        $("#modalErro").modal().show();
                    }
                });                
            }
        });
    }
}

// Modal de criação de um usuário
function openModalCriacaoUsuario() {
    $('#modalCriacaoUsuario').modal({backdrop: 'static', keyboard: false});

    $("#modalCriacaoUsuario").modal().show();

    $("#txtCadastroCPF").mask("000.000.000-00");

    $("#txtCadastroTelefone").mask("(00) 0000-0000");

    $("#frmCadastroUsuario").validate({
        rules: {
            txtCadastroNome: {
                required: true,
                minlength: 5
            },
            txtCadastroEmail: {
                required: true,
                email: true
            },
            txtCadastroCPF: {
                required: true,
                minlength: 14,
                cpfValido: true
            },
            txtCadastroTelefone: {
                required: true,
                minlength: 14
            }
        }, messages: {
            txtCadastroNome: {
                required: "Informe o nome completo do usuário",
                minlength: "O nome do usuário deve conter pelo menos 5 caracteres"
            },
            txtCadastroEmail: {
                required: "Informe o um e-mail válido",
                email: "Não foi informado um e-mail válido"             
            },
            txtCadastroCPF: {
                required: "Informe o CPF do usuário",
                minlength: "Mínimo e máximo para um CPF válido são 11 dígitos",
                cpfValido: "O CPF informado é inválido"
            },
            txtCadastroTelefone: {
                required: "Informe o telefone do usuário",
                minlength: "Informar os 10 dígitos do número do telefone"
            }
        }
    });

    // Adiciona a validação do CPF ao validator
    jQuery.validator.addMethod("cpfValido", function(value) {
        return cpfValido(value);
    });

    $('#btnSalvarCadastroUsuario').unbind("click").bind("click", function () {
        let formularioCadastroValido = $("#frmCadastroUsuario").valid();

        if (formularioCadastroValido) {
            // Dados do usuário
        const dadosPessoa = {
            NomeUsuario: $("#txtCadastroNome").val(),
            EmailUsuario: $("#txtCadastroEmail").val(),
            CPFUsuario: $("#txtCadastroCPF").unmask().val(),
            TelefoneUsuario: $("#txtCadastroTelefone").unmask().val(),
            Ativo: $("#chkCadastroUsuarioAtivo").prop('checked')
        };

        let urlPost = baseUrl.concat("/usuario/criar/");

        $.ajax({
            url: urlPost,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dadosPessoa),
            success: function (result) {
                if (result == true) {
                    fecharModalInclusaoUsuario();
                    $("#modalSucessoInclusao").modal().show();
                    carregarUsuarios();
                } else {
                    $("#modalErro").modal().show();
                }
            },
            error: function () {
                $("#modalErro").modal().show();
            }
        });
    }});
}

// Modal de exclusão do usuário
function openModalExclusaoUsuario(codigoUsuario) {
    if (codigoUsuario > 0) {
        $("#modalExclusao").modal().show();

        $('#btnConfirmaExclusao').unbind().click(function () {

            $('#modalExclusao').modal('hide');

            let urlDelete = baseUrl.concat("/usuario/deletar/" + codigoUsuario);

            $.ajax({
                url: urlDelete,
                method: 'DELETE',
                contentType: 'application/json',
                success: function (result) {
                    if (result == true) {
                        $("#modalSucessoExclusao").modal().show();
                        carregarUsuarios();
                    } else {
                        $("#modalErro").modal().show();
                    }
                },
                error: function () {
                    $("#modalErro").modal().show();
                }
            });
        });
    }
}

function pesquisarUsuarios() {   
    $("#tabelaRegistrosPesquisa").empty();

    document.getElementById('qtdRegistrosPesquisa').style.visibility = 'hidden';
    document.getElementById('lblSemRegistroTabPesquisa').style.visibility = 'hidden';

    let dadosPesquisa = {
        NomeUsuario: $("#txtPesquisaNome").val(),
        CPFUsuario:  $("#txtPesquisaCPF").val(),
        EmailUsuario:  $("#txtPesquisaEmail").val(),
        DataCriacaoInicio:  $("#dtpPesquisaInicial").val(),
        DataCriacaoFim:  $("#dtpPesquisaFinal").val(),
    };

    let urlPostPesquisa = baseUrl.concat("/usuarios-pesquisa/");

    $.ajax({
        url: urlPostPesquisa,
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(dadosPesquisa),
        success: function (data) {
            if (data.length > 0) {
                let cabecalhoTabela =
                    '<thead>' +
                    '<tr>' +
                    '<th scope="col">Nome</th>' +
                    '<th scope="col">CPF</th>' +
                    '<th scope="col">Telefone</th>' +
                    '<th scope="col">E-mail</th>' +
                    '<th scope="col">Data do Cadastro</th>' +
                    '<th scope="col">Ativo</th>' +
                    '<th scope="col">Visualizar</th>' +
                    '<th scope="col">Editar</th>' +
                    '<th scope="col">Excluir</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody> ' +
                    '</tbody>';

                $("#tabelaRegistrosPesquisa").append(cabecalhoTabela);

                for (let i = 0; i < data.length; i++) {
                    let dataFormatada = "";

                    if (data[i].DataCriacao) {
                        let dataRetornada = new Date(data[i].DataCriacao);
                        dataFormatada =
                            dataRetornada.getDate() + "/" +
                            dataRetornada.getMonth() + "/" +
                            dataRetornada.getFullYear() + " às " +
                            dataRetornada.getHours() + ":" +
                            dataRetornada.getMinutes() + "hrs"
                    } else {
                        dataFormatada = "N/D";
                    }

                    let linhaTabela =
                        "<tr>" +
                        "<td>" + data[i].NomeUsuario + "</td>" +
                        "<td>" + cpfMascarado(data[i].CPFUsuario) + "</td>" +
                        "<td>" + telefoneMascarado(data[i].TelefoneUsuario) + "</td>" +
                        "<td>" + data[i].EmailUsuario + "</td>" +
                        "<td>" + dataFormatada + "</td>" +
                        "<td>" + (data[i].Ativo == true ? "Sim" : "Não") + "</td>" +
                        "<td>" +
                        '<button type="button" class="btn btn-info btn-sm" onclick="openModalDetalhamentoUsuario(' + data[i].CodigoUsuario + ')">Detalhes</button>' +
                        "</td>" +
                        "<td>" +
                        '<button type="button" class="btn btn-warning btn-sm" disabled onclick="openModalEdicaoUsuario(' + data[i].CodigoUsuario + ')">Editar</button>' +
                        "</td>" +
                        "<td>" +
                        '<button type="button" class="btn btn-danger btn-sm " disabled onclick="openModalExclusaoUsuario(' + data[i].CodigoUsuario + ')">Excluir</button>' +
                        "</td>" +
                        "</tr>";

                    $("#tabelaRegistrosPesquisa tbody").append(linhaTabela);

                    let labelQtdRegistros = document.getElementById('qtdRegistrosPesquisa');
                    labelQtdRegistros.innerText = "Registro(s) encontrado(s) para esta pesquisa: " + data.length;
                    labelQtdRegistros.style.visibility = 'visible';
                }
            } else {
                document.getElementById('qtdRegistrosPesquisa').style.visibility = 'hidden';
                document.getElementById('lblSemRegistroTabPesquisa').style.visibility = 'visible';
            }
        },
        error: function() {
            $("#modalErro").modal().show();
        }            
    });      
}

// Pega a string do CPF do usuário e força uma máscara
function cpfMascarado(cpf) {
    if (cpf.length == 11) {
        return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4");
    }
}

// Pega a string do telefone do usuário e força uma máscara
function telefoneMascarado(telefone) {
    if (telefone.length == 10) {
        var cleaned = ('' + telefone).replace(/\D/g, '');
        var match = cleaned.match(/^(\d{2})(\d{4})(\d{4})$/);
        if (match) {
            return '(' + match[1] + ') ' + match[2] + '-' + match[3];
        }
        return null;
    }
}

// Previne possíveis sujeiras no formulário - Cadastro
function fecharModalInclusaoUsuario() {
    $('#frmCadastroUsuario')[0].reset();
    $("#modalCriacaoUsuario").modal('hide');
    $("#frmCadastroUsuario").validate().resetForm();    
}

// Previne possíveis sujeiras no formulário - Edição
function fecharModalEdicaoUsuario() {
    $("#modalEdicaoUsuario").modal('hide');
    $("#frmEdicaoUsuario").validate().resetForm();
}

// Útil - Validação CPF
function cpfValido(cpf) {
    exp = /\.|-/g;
    cpf = cpf.toString().replace(exp, "");
    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0,
            soma2 = 0;
    var vlr = 11;
    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) === 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);
    if (cpf === "11111111111" || cpf === "22222222222" || cpf === "33333333333" || cpf === "44444444444" || cpf === "55555555555" || cpf === "66666666666" || cpf === "77777777777" || cpf === "88888888888" || cpf === "99999999999" || cpf === "00000000000") {
        var digitoGerado = null;
    } else {
        var digitoGerado = (soma1 * 10) + soma2;
    }
    if (digitoGerado !== digitoDigitado) {
        return false;
    }
    return true;
}