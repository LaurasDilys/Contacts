import Chip from '@mui/material/Chip';

const stringToColor = string => {
  let hash = 0;
  let i;

  for (i = 0; i < string.length; i++) {
    hash = string.charCodeAt(i) + ((hash << 5) - hash);
  }

  let color = '#';

  for (i = 0; i < 3; i++) {
    const value = (hash >> (i * 8)) & 0xff;
    color += `00${value.toString(16)}`.substr(-2);
  }

  return color;
}

const Test = () => {

  return (
    <div style={{display: 'flex', justifyContent: 'center', height: '100vh'}}>
      <Chip label='test' sx={{bgcolor: stringToColor('test'), color: 'white'}} onDelete={() => {}} />
    </div>
  );
}

export default Test;