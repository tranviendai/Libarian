import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom";
import { CallApiWithToken } from "../utils/callApi";
import useGlobalContext from "../contexts/GlobalContext";
import useFetch from '../utils/useFetch';
import {convertToDMY, convertToYMD} from "../utils/convertDate";

const PutBookPage = () => {
    const { id } = useParams();
    const { token } = useGlobalContext();

    const DefaultFormData = { title: '', author: '', publisher: '', publishingYear: '', summary: '', categoryID: '-1' }
    const [form, setForm] = useState(DefaultFormData);

    const { data: categories } = useFetch('/categories');
    let { data: book } = useFetch('/books/' + id)

    useEffect(() => {
        if (!id || !book) return;
        setForm({
            ...book,
            publishingYear: convertToYMD(book.publishingYear),
        });
    }, [book, id])

    const navigate = useNavigate();

    const onFormChange = (e) => {
        const { id, value } = e.target;
        setForm(x => { return { ...x, [id]: value } })
    }

    const onSubmit = async () => {
        try {

            if (form.categoryID < 0) {
                alert('Vui lòng chọn thể loại');
                return;
            }
            const body = {
                ...form,
                publishingYear: convertToDMY(form.publishingYear),
                bookID: id || 'id',
                image: 'img',
            }

            if (id) {
                await CallApiWithToken(token).put('/books/' + id, body)
            } else { 
                await CallApiWithToken(token).post('/books/', body)
            }

            alert('Thành công!')
            if (id) navigate('/BLibrary/Book/' + id);
            else setForm(DefaultFormData);
        } catch (err) {
            console.log(err);
            alert('Có lỗi!');
        }
    }

    return <div className="put-book-page">
        <div className="container-80">
            <div className="page-title">{id ? 'Sửa sách' : 'Thêm Sách'}</div>
            <div className="w-75 p-3 mx-auto">
                <form action="#">
                    <div className="row mb-3">
                        <div className="form-floating col-6">
                            <input type="text" id="title" className="form-control" placeholder=" "
                                onChange={onFormChange} value={form && form['title']} />
                            <label htmlFor="title" className="ps-4">Tên sách: </label>
                        </div>
                        <div className="form-floating col-6">
                            <input type="text" id="author" className="form-control" placeholder=" "
                                onChange={onFormChange} value={form && form['author']} />
                            <label htmlFor="author" className="ps-4">Tác giả:</label>
                        </div>
                    </div>

                    <div className="row mb-3">
                        <div className="form-floating col-6">
                            <input type="text" id="publisher" className="form-control" placeholder=" "
                                onChange={onFormChange} value={form && form['publisher']} />
                            <label htmlFor="publisher" className="ps-4">Nhà xuất bản:</label>
                        </div>
                        <div className="form-floating col-3">
                            <input type="date" id="publishingYear" className="form-control" placeholder=" "
                                onChange={onFormChange} value={form && form['publishingYear']} />
                            <label htmlFor="publishingYear" className="ps-4">Năm xuất bản:</label>
                        </div>
                        <div className="form-floating col-3">
                            <select id="categoryID" className="form-select" onChange={onFormChange} value={form && form['categoryID']}>
                                <option key={-1} value={-1} hidden>Chọn thể loại</option>
                                {categories?.map(x => <option key={x.categoryID} value={x.categoryID}>{x.nameCategory}</option>)}
                            </select>
                            <label htmlFor="categoryID" className="ps-4">Thể loại:</label>
                        </div>
                    </div>

                    <div className="mb-3">
                        <label htmlFor="summary" className="form-label">Tóm tắt:</label>
                        <textarea id="summary" className="form-control" onChange={onFormChange} value={form && form['summary']} />
                    </div>
                    <div className="btn btn-submit" onClick={onSubmit}>Xác nhận</div>
                </form>
            </div>
        </div>
    </div>
}

export default PutBookPage

// import { useEffect, useState } from "react"
// import { useNavigate, useParams } from "react-router-dom";
// import CallApi, {CallApiWithToken} from "../utils/callApi";
// import {convertToDMY, convertToYMD} from "../utils/convertDate";
// import useGlobalContext from "../contexts/GlobalContext";

// const PutBookPage = () => { 
//     const { id } = useParams();
//     const { token } = useGlobalContext();

//     const DefaultFormData = { title: '', author: '', publisher: '', publishingYear: '2000-01-01', summary: '', categoryID: '-1' }
//     const [form, setForm] = useState(DefaultFormData);
//     const [categories, setCategories] = useState([]);
//     const navigate = useNavigate();

//     //category
//     useEffect(() => { 
//         let mounted = true;

//         const fetchApi = async () => { 
//             try {
//                 const resp = await CallApi.get('/categories');
//                 const data = resp.data;
//                 if (mounted) setCategories(data);
//             } catch (err) { 
//                 console.log(err);
//             }
//         }

//         fetchApi();

//         return () => { 
//             mounted = false;
//         }
//     }, [])

//     //book
//     useEffect(() => { 
//         let mounted = true;

//         const fetchApi = async () => { 
//             try {
//                 const resp = CallApi.get('/books/' + id);
//                 const data = (await resp).data;
//                 const book = data;

//                 if(mounted) setForm({
//                     title: book.title,
//                     author: book.author,
//                     publisher: book.publisher,
//                     publishingYear: convertToYMD(book.publishingYear),
//                     summary: book.summary,
//                     categoryID: book.categoryID
//                 })
//             } catch (err) { 
//                 console.log(err)
//             }
//         }

//         if (id) fetchApi();

//         return () => { 
//             mounted = false;
//         }
//     }, [id])

//     const onFormChange = (e) => { 
//         const { id, value } = e.target;
//         setForm(x => { return { ...x , [id]: value} })
//     }

//     const onSubmit = async () => { 
//         try {

//             if (form.categoryID < 0) { 
//                 alert('Vui lòng chọn thể loại');
//                 return;
//             }
//             const body = {
//                 ...form,
//                 publishingYear: convertToDMY(form.publishingYear),
//                 bookID: id || 'id',
//                 image: 'img'
//             }
//             if (id) {
//                 await CallApiWithToken(token).put('/books/' + id, body)
//             } else { 
//                 await CallApiWithToken(token).post('/books/', body)
//             }

//             alert('Thành công!')
//             if (id) navigate('/BLibrary/Book/' + id);
//             else setForm(DefaultFormData);
//         } catch (err) { 
//             console.log(err);
//             alert('Có lỗi!');
//         }
//     }

//     return <div className="put-book-page">
//         <div className="container-80">
//             <div className="page-title">{id ? 'Sửa sách' :'Thêm Sách'}</div>
//             <div className="flex-center">
//                 <form action="#">
//                     <div>
//                         <label htmlFor="title">Tên sách: </label>
//                         <input type="text" id="title" onChange={onFormChange} value={form['title']} />
//                     </div>
//                     <div>
//                         <label htmlFor="author">Tác giả</label>
//                         <input type="text" id="author" onChange={onFormChange} value={form['author']} />
//                     </div>
//                     <div>
//                         <label htmlFor="publisher">Nhà xuất bản</label>
//                         <input type="text" id="publisher" onChange={onFormChange} value={form['publisher']} />
//                     </div>
//                     <div>
//                         <label htmlFor="publishingYear">Năm xuất bản</label>
//                         <input type="date" id="publishingYear" onChange={onFormChange} value={form['publishingYear']} />
//                     </div>
//                     <div>
//                         <label htmlFor="summary">Tóm tắt</label>
//                         <input type="text" id="summary" onChange={onFormChange} value={form['summary']} />
//                     </div>
//                     <div>
//                         <label htmlFor="categoryID">Thể loại</label>
//                         <select id="categoryID" onChange={onFormChange} value={form['categoryID']}>
//                             <option key={-1} value={-1} hidden>Chọn thể loại</option>
//                             {categories.map(x => <option key={x.categoryID} value={x.categoryID}>{x.nameCategory}</option>)}
//                         </select>
//                     </div>
//                     <div className="btn btn-submit" onClick={onSubmit}>Xác nhận</div>
//                 </form>
//             </div>
//         </div>
//     </div> 
// }

// export default PutBookPage