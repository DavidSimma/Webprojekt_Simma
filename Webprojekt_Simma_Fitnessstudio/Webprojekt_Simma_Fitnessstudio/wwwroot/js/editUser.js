

$(document).ready(() => {
    $.ajax({
        url: "/user/GetUser",
        method: "GET",
        success: (data) => {
            if (data == "nicht angemeldet") {
                $("#userTable").text("Sie sind immer noch nicht angemeldet!");
            }
            if (data == "Error") {
                $("#userTable").text("Es ist ein Fehler aufgetreten!");
            }
            else {
                $("#userTable").html(displayUserTable(data));
            }
        },
        error: () => {
            $("#userTable").text("Es trat ein Serverproblem auf!");
        }
    })
});

function displayUserTable(user) {
    let value = JSON.parse(user);
    let s = `
    <table>
        <thead>
            <tr>
                <td><center>Benutzer</center></td>
            </tr>
        </thead>
        <tbody>
            <tr>
                    <td class="displayer">Benutzername: </td>
                    <td>${value.UserName}</td>
                </tr>
                <tr>
                    <td class="displayer">Vorname: </td>
                    <td><input value="${value.Firstname}" /></td>
                </tr>
                <tr>
                    <td class="displayer">Nachname: </td>
                    <td><input value="${value.Lastname}" /></td>
                </tr>
                <tr>
                    <td class="displayer">Geburtsdatum: </td>
                    <td><input value="${displayDate(value.Age)[0]}" type="date"/></td>
                    
                </tr>
                <tr>
                    <td class="displayer">Geschlecht: </td>
                    <td>${displayGender(value.Gender)}</td>
                </tr>
        </tbody>
    </table>`;
    return s;
}

function displayGender(g) {
    if (g == 0) {
        return `<select>
            <option value="0" selected="selected">männlich</option>
            <option value="1">weiblich</option>
        </select>`;
    }
    else if (g==1){
        return `<select>
            <option value="0" >männlich</option>
            <option value="1" selected="selected">weiblich</option>
        </select>`;
    }
}

function displayDate(date) {
    let s = date.split("T");
    return s;
}