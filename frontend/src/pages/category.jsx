import { useEffect, useState } from 'react'
import CallApi from '../utils/callApi'
import FrmAddCate from '../components/page/category/frm-add-cate'
import useGlobalContext from '../contexts/GlobalContext'
import CategoriesGrid from '../components/shared/categories-grid'
import BG from '../resources/imgs/cate_bg.png'

const { CategoriesData } = require('../mock-data')

const CategoryPage = () => { 
    const [list, setList] = useState([]);
    const [showFrmAdd, setShowFrmAdd] = useState(false);
    const [refresh, setRefresh] = useState(false);
    const [selectedItem, setSelectedItem] = useState({});
    const [loading, setLoading] = useState(false);

    const { token } = useGlobalContext();

    useEffect(() => { 
        let mounted = true;

        const fetchApi = async () => { 
            setLoading(true);
            try {
                const resp = await CallApi.get('/categories');
                const data = resp.data;
                if (mounted) setList(data);
            } catch (err) {
                console.log('api failed', err);
                if (mounted) setList(CategoriesData);
            } finally { 
                setLoading(false);
            }
        }

        fetchApi();

        return () => { 
            mounted = false
        }
    }, [refresh])

    const showFrm = () => { 
        if (!token) return;
        setShowFrmAdd(true);
    }

    const onAdd = () => { 
        setSelectedItem({});
        showFrm();
    }

    return token ? <div className='category-page'>
        {showFrmAdd && <FrmAddCate setShow={setShowFrmAdd} item={selectedItem} setRefresh={setRefresh} />}
        <div className="container-80">
            <div className="header">
                <div className="title">Danh Sách Thể Loại</div>
                <div className="btn btn-add" onClick={onAdd}>
                    THÊM
                </div>
            </div>

            {loading && <div className="loader"></div>}
            <CategoriesGrid list={list} onSelect={(x) => { setSelectedItem(x); showFrm(); }} />
        </div>
    </div> : <div className="category-page">
            <div className="content">
                <div className="left">
                    <div className="header">
                        <div className="title">Các thể loại sách có trong B - Library</div>
                    </div>

                    <CategoriesGrid list={list} nCol={4} />
                </div>
                <div className="right">
                    <img src={BG} alt="" />
                </div>
            </div>
    </div>
}

export default CategoryPage;

{/* {showFrmAdd && <FrmAddCate setShow={setShowFrmAdd} item={selectedItem} setRefresh={setRefresh} />}
        <h2 className="title">Danh Sách thể loại</h2>
        <div className='container-80'>
            {token && <>
                <div className="btn-add btn" onClick={onAdd}>+ Thêm thể loại</div>
                <p><i>Click vào thể loại để chỉnh sửa</i></p>
            </>}
            
                {loading ?
                    <div className="loader"></div>
                    : <>
                        <div className="category-list ">
                        {list.map(x => <div key={x.categoryID} className='category-item btn' onClick={() => { setSelectedItem(x); showFrm(); }} >
                                <div className="name">{x.nameCategory}</div>
                            </div>)}
                        </div>
                    </>
                }
        </div> */}