const API_BASE = "/api";
let editingRecordId = null;

async function loadEmployers() {
  const response = await fetch(`${API_BASE}/Employers`);
  const employers = await response.json();

  const list = document.getElementById("employer-list");
  list.innerHTML = "";

  employers.forEach(emp => {
    const li = document.createElement("li");
    li.textContent = `${emp.name} - ${emp.address ?? ""} - ${emp.phone ?? ""}`;
    list.appendChild(li);
  });

  fillSelect("record-employer", employers, "id", "name");
  return employers;
}

loadEmployers();

document.getElementById("employer-form").addEventListener("submit", async (event) => {
  event.preventDefault();

  const newEmployer = {
    name: document.getElementById("employer-name").value,
    address: document.getElementById("employer-address").value,
    phone: document.getElementById("employer-phone").value
  };

  await fetch(`${API_BASE}/Employers`, {
    method: "POST",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(newEmployer)
  });

  document.getElementById("employer-form").reset();
  await loadEmployers();
})

async function loadWorkers() {
  const response = await fetch(`${API_BASE}/Workers`);
  const workers = await response.json();

  const list = document.getElementById("worker-list");
  list.innerHTML = "";

  workers.forEach(w => {
    const li = document.createElement("li");
    li.textContent = `${w.fullName} - ${w.phone ?? ""} - ${w.dailyWage ?? ""}`;
    list.appendChild(li);
  });

  fillSelect("record-worker", workers, "id", "fullName");
  return workers;
}

loadWorkers();

document.getElementById("worker-form").addEventListener("submit", async(event) => {
  event.preventDefault();

  const newWorker = {
    fullName: document.getElementById("worker-name").value,
    phone: document.getElementById("worker-phone").value,
    dailyWage: document.getElementById("worker-wage").value
      ? Number(document.getElementById("worker-wage").value) : null
  };

  await fetch(`${API_BASE}/Workers`, {
    method: "POST",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(newWorker)
  });

  document.getElementById("worker-form").reset();
  await loadWorkers();

})

function fillSelect(selectId, items, valueField, textField) {
  const select = document.getElementById(selectId);
  select.innerHTML = "";

  items.forEach(item => {
    const option = document.createElement("option");
    option.value = item[valueField];
    option.textContent = item[textField];
    select.appendChild(option);
  });
}

async function loadDailyRecords() {
  const response = await fetch(`${API_BASE}/DailyRecords`);
  const records = await response.json();

  const tbody = document.getElementById("record-table-body"); 
  tbody.innerHTML  = "";

  records.forEach(r => {
    const row = document.createElement("tr");
    
    row.innerHTML = `
      <td>${r.date}</td>
      <td>${r.employer?.name ?? ""}</td>
      <td>${r.worker?.fullName ?? ""}</td>
      <td>${r.note ?? ""}</td>
      <td>
        <button onclick="startEditRecord(${r.id}, '${r.date}', ${r.employerId}, ${r.workerId}, '${(r.note ?? "").replace(/'/g, "\\'")}')">Düzenle</button>
        <button onclick="deleteDailyRecord(${r.id})">Sil</button>
    </td>
    `;

    tbody.appendChild(row);
  });
}

loadDailyRecords();

async function deleteDailyRecord(id) {
  await fetch(`${API_BASE}/DailyRecords/${id}`, {
    method: "DELETE"
  });

  await loadDailyRecords();
}

document.getElementById("record-form").addEventListener("submit", async (event) => {
  event.preventDefault();

  const recordData = {
    date: document.getElementById("record-date").value,
    employerId: Number(document.getElementById("record-employer").value),
    workerId: Number(document.getElementById("record-worker").value),
    note: document.getElementById("record-note").value
  };
  let response;

  if(editingRecordId) {
      response = await fetch(`${API_BASE}/DailyRecords/${editingRecordId}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(recordData)
      }); 
  } else {
    response = await fetch(`${API_BASE}/DailyRecords`, {
      method: "POST",
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify(recordData)
  }); 
  }
  

  if(!response.ok) {
    alert("Kayit eklenemedi. Ayni isci, ayni isverende, ayni gun zaten kayitli olabilir.");
    return;
  }

  editingRecordId = null;
  document.querySelector("#record-form button[type='submit']").textContent = "Ekle";

  document.getElementById("record-form").reset();
  await loadDailyRecords();
})

function startEditRecord(id, date, employerId, workerId, note) {
  editingRecordId = id;

  document.getElementById("record-date").value = date;
  document.getElementById("record-employer").value = employerId;
  document.getElementById("record-worker").value = workerId;
  document.getElementById("record-note").value = note;

  document.querySelector("#record-form button[type='submit']").textContent = "Guncelle";
}


