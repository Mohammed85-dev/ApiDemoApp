var add_pic = document.getElementById("account-add-profile-picture");
var pic_value = document.getElementById("images");
var form = document.getElementById("upload");
var profile_pic = document.getElementById("account-profile-picture");

const handleSubmit = async (event) => {
    event.preventDefault();
    const form = document.getElementById("upload");
    const formData = new FormData(form);
    const image = formData.get("image");
    uploadFile(image);
};

const uploadFile = async (file) => {
    console.log(`Uploading ${file.name}...`);

    const formData = new FormData();

    formData.append("file", file);
//   formData.append("upload_preset", "<Your Unsigned Upload Preset>");
    // formData.append("upload_preset", "<Your Signed Upload Preset>")

    const response = await fetch(
        "https://localhost:7222/api/Users/Avatar/" +
        "f0d3a648-828e-45c6-940b-2132f0e2d93a",
        {
            method: "PUT",
            headers: {
                Authorization: "n6stqYsvtrvs4-IsIN3ZogxBnxwcmEhRQ3pIO8um-vM",
            },
            body: formData,
        }
    ).catch((error) => console.error(error));

    const x = await response.json();
    console.log(x.url);
    return x;
};

form.addEventListener("submit", handleSubmit);

// pic_value.addEventListener("change", async (e) => {
//   const file = e.target.files[0];
/*if (!file) return;

    // Method 1: Using FileReader
    const bytes = await new Promise((resolve) => {
        const reader = new FileReader();
        reader.onload = (event) => {
        const arrayBuffer = event.target.result;
        resolve(new Uint8Array(arrayBuffer)); // Convert to byte array
        };
        reader.readAsArrayBuffer(file);
    });

    console.log(bytes);*/

//   const reader = new FileReader();

//   reader.addEventListener("load", () => {
//     console.log(reader.result);
//   });

//   reader.readAsDataURL(file);

//   fetch(
//     "https://localhost:7222/api/Users/Avatar/" +
//       "f0d3a648-828e-45c6-940b-2132f0e2d93a",
//     {
//       method: "PUT",
//       headers: {
//         Authorization: "n6stqYsvtrvs4-IsIN3ZogxBnxwcmEhRQ3pIO8um-vM",
//       },
//       body: bytes,
//     }
//   ).catch((error) => console.error(error));
// });

