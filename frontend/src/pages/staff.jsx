import useFetch from '../utils/useFetch';
import DataTable from '../components/shared/data-table';
import { useNavigate } from 'react-router-dom';
import useGlobalContext from '../contexts/GlobalContext';

const StaffPage = () => {

    const { token } = useGlobalContext();
    const navigate = useNavigate();

    const { data: emps, loading } = useFetch('/staffs', null, token);
    const headers = ['Tên', 'Giới tính','Địa chỉ', 'Email', 'Ngày sinh'];
    const rows = emps?.map(x => {
        return {
            onRowSelected: () => navigate('/BLibrary/UpdateEmp/' + x.id),
            rowData: [x.fullName, x.sex, x.address, x.email, x.birthday]
        }
    })

    return <div className="container-80">
        <div className="page-title">Danh sách thủ thư</div>

        <div className='mb-3'>
            <div className="btn btn-primary" onClick={() => navigate('/BLibrary/AddEmp/')}>Thêm mới</div>
        </div>
        {loading ? <div className="loader"></div> :
            emps && emps.length ? <DataTable headers={headers} rows={rows} /> :
                <p><i>Không tìm thấy</i></p>
        }
    </div>
}

export default StaffPage;