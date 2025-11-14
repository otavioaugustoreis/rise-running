const API = "https://localhost:7141/Runners";

const inputCPF = document.getElementById("cpf");
inputCPF.addEventListener("input", () => {
    let v = inputCPF.value.replace(/\D/g, "");

    if (v.length <= 11) {
        inputCPF.value = v
            .replace(/(\d{3})(\d)/, "$1.$2")
            .replace(/(\d{3})(\d)/, "$1.$2")
            .replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    }
});

document.getElementById("btnRegistrar").addEventListener("click", async () => {
    const nome = document.getElementById("nome").value.trim();
    const cpf = document.getElementById("cpf").value.replace(/\D/g, "");

    if (!nome || cpf.length !== 11) {
        return abrirModal("Preencha o nome e CPF válido!");
    }

    try {
        const response = await fetch(API, {
            method: "POST",
            headers: { nome: nome, cpf: cpf }
        });

        const msg = await response.text();
        abrirModal(msg);

        await carregarLista();

    } catch {
        abrirModal("Erro ao comunicar com o servidor.");
    }
});

async function carregarLista() {
    const tabela = document.getElementById("listaRunners");
    tabela.innerHTML = "";

    try {
        const response = await fetch(API);
        const data = await response.json();

        data.forEach((runner, index) => {
            const tr = document.createElement("tr");

            tr.innerHTML = `
                <td>${index + 1}</td>
                <td>${runner.nome}</td>
                <td>${formatarCPF(runner.cpf)}</td>
            `;

            tabela.appendChild(tr);
        });

    } catch {
        abrirModal("Erro ao carregar lista.");
    }
}

function formatarCPF(cpf) {
    cpf = cpf.toString();
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
}

function abrirModal(mensagem) {
    const modal = document.getElementById("modal");
    document.getElementById("modalMessage").textContent = mensagem;
    modal.style.display = "block";
}

document.getElementById("closeModal").onclick = () =>
    document.getElementById("modal").style.display = "none";
