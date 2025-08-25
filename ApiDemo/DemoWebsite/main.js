const setupInput = () => {
    const input = document.getElementById('images');
    input.addEventListener('change', handleChange)
}

const setupForm = () => {
    const form = document.getElementById('upload');
    form?.addEventListener('submit', handleSubmit)
}

const handleChange = async (event) => {
    const {files} = event.target

    if (!files || files.length === 0) {
        clearImagesToUpload()
        return
    }

    setImagesToUpload(files)
    clearUploadedImages()
}

const handleSubmit = async (event) => {
    event.preventDefault()

    const form = document.getElementById("upload")
    const formData = new FormData(form)
    const images = formData.getAll("images")

    setLoading()
    const uploadedImages = await uploadImages(images)
    clearLoading()

    setUploadedImages(uploadedImages)

    clearForm()
    clearImagesToUpload()
}

const uploadImages = async (images) => {
    const promises = images.map(image => uploadFile(image))
    const uploadedImages = await Promise.all(promises)

    return uploadedImages
}

const uploadFile = async (file) => {
    console.log(`Uploading ${file.name}...`)

    const formData = new FormData()

    formData.append("file", file)
    // formData.append("upload_preset", "<Your Unsigned Upload Preset>")
    // formData.append("upload_preset", "<Your Signed Upload Preset>")

    const response = await fetch(
        "https://localhost:7222/api/Users/Avatar/f0d3a648-828e-45c6-940b-2132f0e2d93a",
        {
            method: "PUT",
            headers: {
                "Authorization": "n6stqYsvtrvs4-IsIN3ZogxBnxwcmEhRQ3pIO8um-vM"
            },
            body: formData,
        },
    )

    const image = await response.json()
    console.log(image.url)
    return image
}

const clearUploadedImages = () => {
    const uploadedImages = document.getElementById(
        "uploadedImages",
    )

    uploadedImages.innerHTML = "No images uploaded yet."
}


const setLoading = () => {
    const button = document.getElementById("submit")
    const input = document.getElementById("images")

    button.setAttribute("disabled", "true")
    button.innerText = "Uploading..."
    input.setAttribute("disabled", "true")
}

const clearLoading = () => {
    const button = document.getElementById("submit")
    const input = document.getElementById("images")

    button.removeAttribute("disabled")
    button.innerText = "Upload"
    input.removeAttribute("disabled")
}

const setUploadedImages = (images) => {
    const uploadedImages = document.getElementById(
        "uploadedImages",
    )

    uploadedImages.innerHTML = ""

    images.forEach(image => {
        const cldImage = cld
            .image(image.public_id)
            .format("auto")
            .quality("auto")
            .resize(scale().width(225))

        const uploadedImage = document.createElement("img")
        uploadedImage.src = cldImage.toURL()
        uploadedImages.appendChild(uploadedImage)
    })
}

const clearForm = () => {
    const form = document.getElementById("upload")

    form.reset()
}

const clearImagesToUpload = () => {
    const imagesToUpload = document.getElementById(
        "imagesToUpload",
    )

    imagesToUpload.innerHTML = "No images selected yet."
}

const setImagesToUpload = (files) => {
    const imagesToUpload = document.getElementById(
        "imagesToUpload",
    )

    const imageList = document.createElement("ul")

    for (const file of files) {
        const selectedImage = document.createElement("li")
        selectedImage.innerHTML = file.name
        imageList.appendChild(selectedImage)
        imagesToUpload.innerHTML = imageList.outerHTML
    }
}

setupInput()
setupForm()